using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using MongoDBBson = MongoDB.Bson;

namespace Orleans.EventSourcing.MongoDB
{
    public class MongoDbEventStore : IEventStore
    {
        private readonly IMongoDatabase _mongoDatabase;
        private const string COLLECTION_NAME = "event";
        private static bool _hasIndex;
        public MongoDbEventStore(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<IEvent>> ReadFromAsync(string grainId, long eventVersion = 0)
        {
            var collection = (await GetCollection(COLLECTION_NAME));
            var filter = BsonDocument.Parse("{ GrainId:\"" + grainId + "\",Version:{ $gte: " + eventVersion + " }}");
            var sort = BsonDocument.Parse("{ Version:1 }");
            var options = new FindOptions<BsonDocument, BsonDocument>
            {
                AllowPartialResults = false,
                BatchSize = 20,
                Sort = sort
            };
            using (var cursor = await collection.FindAsync(filter, options))
            {
                var eventsBson = await cursor.ToListAsync();

                var events = eventsBson.Select(ConvertJsonToEvent);

                return events;
            }
        }

        public async Task<IEvent> ReadOneAsync(string grainId, string commandId)
        {
            var collection = (await GetCollection(COLLECTION_NAME)).WithReadPreference(ReadPreference.SecondaryPreferred);
            var filter = BsonDocument.Parse("{ GrainId:\"" + grainId + "\",CommandId:" + commandId + "}");
            var options = new FindOptions<BsonDocument, BsonDocument>
            {
                AllowPartialResults = false,
                Limit = 1
            };
            using (var cursor = await collection.FindAsync(filter, options))
            {
                var eventsBson = await cursor.ToListAsync();

                var events = eventsBson.Select(ConvertJsonToEvent);

                return events.FirstOrDefault();
            }
        }

        public async Task<EventWriteResult> AppendAsync(IEvent @event)
        {
            var collection = (await GetCollection(COLLECTION_NAME)).WithWriteConcern(new WriteConcern(new Optional<WriteConcern.WValue>(), journal: new Optional<bool?>(true)));
            var json = JsonConvert.SerializeObject(@event);
            var result = EventWriteResult.UnknowError;


            var doc = BsonSerializer.Deserialize<BsonDocument>(json);
            try
            {
                await collection.InsertOneAsync(doc);
                result = EventWriteResult.Success;
            }
            catch (MongoDuplicateKeyException ex)
            {
                result = EventWriteResult.Duplicate;
            }

            return result;
        }


        private async Task<IMongoCollection<BsonDocument>> GetCollection(string name)
        {
            var collection = _mongoDatabase.GetCollection<BsonDocument>(name);

            if (!_hasIndex)
            {
                using (var cursor = await collection.Indexes.ListAsync())
                {
                    var indexes = await cursor.ToListAsync();
                    var versionIndex = "grainid_version_index";
                    var commandIdIndex = "grainid_commandid_typecode_index";
                    if (indexes.Count(index => index["name"] == versionIndex) == 0)
                    {
                        var indexGrainIdVersion = Builders<BsonDocument>.IndexKeys.Ascending("GrainId").Ascending("Version");
                        await collection.Indexes.CreateOneAsync(indexGrainIdVersion, new CreateIndexOptions() { Unique = true, Name = versionIndex });
                    }
                    //if (indexes.Count(index => index["name"] == commandIdIndex) == 0)
                    //{
                    //    var indexGrainIdVersion = Builders<BsonDocument>.IndexKeys.Ascending("GrainId").Ascending("CommandId").Ascending("TypeCode");
                    //    await collection.Indexes.CreateOneAsync(indexGrainIdVersion, new CreateIndexOptions() { Unique = true, Name = commandIdIndex });
                    //}
                    _hasIndex = true;
                }
            }
            return collection;
        }

        private IEvent ConvertJsonToEvent(BsonDocument bson)
        {
            bson.Remove("_id");
            int eventTypeCode = -1;
            var json = bson.ToJson();
            eventTypeCode = bson.GetValue("TypeCode", BsonValue.Create(-1)).AsInt32;
            Type eventType;

            if (eventTypeCode < 0 || !EventTypeCodeMapping.TryGetEventType(eventTypeCode, out eventType))
            {
                throw new Exception("unknow event type");
            }

            var convertEvent = JsonConvert.DeserializeObject(json, eventType) as IEvent;

            return convertEvent;
        }
    }
}
