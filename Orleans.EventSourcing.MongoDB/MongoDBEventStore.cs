﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDBBson = MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Orleans.EventSourcing.MongoDB
{
    public class MongoDBEventStore : IEventStore
    {
        private readonly IMongoDatabase _mongoDatabase;
        private const string CollectionName = "event";
        private static bool HasIndex = false;
        public MongoDBEventStore(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<IEnumerable<IEvent>> ReadFrom(string grainId, ulong eventId = 0)
        {
            var collection = (await GetCollection(CollectionName)).WithReadPreference(ReadPreference.SecondaryPreferred);
            var filter = MongoDBBson.BsonDocument.Parse("{ GrainId:\"" + grainId + "\",Version:{ $gte: " + eventId + " }}");
            var sort = MongoDBBson.BsonDocument.Parse("{ Version:1 }");
            var options = new FindOptions<MongoDBBson.BsonDocument, MongoDBBson.BsonDocument>
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

        public async Task Append(IEvent @event)
        {
            var collection = (await GetCollection(CollectionName)).WithWriteConcern(new WriteConcern(new Optional<WriteConcern.WValue>(), journal: new Optional<bool?>(true)));
            var json = JsonConvert.SerializeObject(@event);

            var doc = MongoDBBson.Serialization.BsonSerializer.Deserialize<BsonDocument>(json);
            await collection.InsertOneAsync(doc);
        }


        private async Task<IMongoCollection<MongoDBBson.BsonDocument>> GetCollection(string name)
        {
            var collection = _mongoDatabase.GetCollection<MongoDBBson.BsonDocument>(name);

            if (!HasIndex)
            {
                using (var cursor = await collection.Indexes.ListAsync())
                {
                    var indexes = await cursor.ToListAsync();
                    if (indexes.Count(index => index["name"] == "GrainId_1_Version_1") == 0)
                    {
                        var keys = Builders<MongoDBBson.BsonDocument>.IndexKeys.Ascending("GrainId").Ascending("Version");
                        await collection.Indexes.CreateOneAsync(keys);
                    }
                    HasIndex = true;
                }
            }
            return collection;
        }

        private IEvent ConvertJsonToEvent(MongoDBBson.BsonDocument bson)
        {
            bson.Remove("_id");
            var json = bson.ToJson();
            dynamic @event = JsonConvert.DeserializeObject(json);
            uint eventTypeCode = @event.TypeCode;
            Type eventType;

            if (!EventNameTypeMapping.TryGetEventType(eventTypeCode, out eventType))
            {
                throw new Exception("unknow event type");
            }

            var convertEvent = JsonConvert.DeserializeObject(json, eventType) as IEvent;

            return convertEvent;
        }
    }
}