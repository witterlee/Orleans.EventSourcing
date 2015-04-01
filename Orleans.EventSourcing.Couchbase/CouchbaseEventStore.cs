using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Core;
using Newtonsoft.Json;

namespace Orleans.EventSourcing.Couchbase
{
    public class CouchbaseEventStore : IEventStore
    {
        private static IBucket _bucket;
        //private static bool hasDesignDoc = false;
        private const string DESGIN_DOC_NAME = "__eventsource_desgin_doc";
        private const string VIEW_NAME = "eventview";
        //private const string desginDoc = "{\"views\":{\"" + VIEW_NAME + "\":{\"map\":\"function (doc, meta) { if(doc.GrainId!=undefined&&doc.Version>0&&doc.Type!=undefined) emit([doc.GrainId, doc.Version]); }\"}}}";
        //private static IBucketManager bucketManager;
        //private static bool hasDesignDoc; 
        public CouchbaseEventStore(IBucket bucket, string couchbaseUser = "", string couchbasePwd = "")
        {
            if (_bucket == null)
                _bucket = bucket;
            //if (bucketManager == null)
            //    bucketManager = _bucket.CreateManager(couchbaseUser, couchbasePwd);

        }

        public async Task<IEnumerable<IEvent>> ReadFrom(string grainId, ulong eventId = 0)
        {
            //if (!hasDesignDoc)
            //{
            //    var designDoc = await bucketManager.GetDesignDocumentAsync(DESGIN_DOC_NAME);
            //    if (!designDoc.Success)
            //    {
            //        var createResult = await bucketManager.InsertDesignDocumentAsync(DESGIN_DOC_NAME, desginDoc);
            //        if (createResult.Success) hasDesignDoc = true;
            //    }
            //}

            var startKey = new object[] { grainId.ToString(), eventId };
            var endKey = new object[] { grainId.ToString(), int.MaxValue };
            var query = _bucket.CreateQuery(DESGIN_DOC_NAME, VIEW_NAME)
                               .StartKey(startKey)
                               .EndKey(endKey);

            var result = new List<IEvent>();

            var indexs = await _bucket.QueryAsync<dynamic>(query);

            var keys = indexs.Rows.Select(r => r.Id).ToList();

            if (keys.Count > 0)
            {
                var tcs = new TaskCompletionSource<List<IEvent>>();

#pragma warning disable 4014
                Task.Factory.StartNew(() =>
#pragma warning restore 4014
                {
                    try
                    {
                        var events = _bucket.Get<string>(keys);

                        if (events.Count(e => e.Value.Success) == keys.Count)
                        {
                            var eventsResult = events.Select(et => ConvertJsonToEvent(et.Value.Value)).ToList();

                            tcs.SetResult(eventsResult);
                        }
                        else
                        {
                            Exception firstEx = null;
                            foreach (var et in events)
                            {
                                firstEx = et.Value.Exception ?? null;
                                if (firstEx != null)
                                    break;
                            }
                            tcs.SetException(new Exception("Read events from couchbase error", firstEx));
                        }
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(new Exception("Read event to store exception", ex));
                    }
                });

                result = await tcs.Task;
            }

            return result;
        }

        public async Task Append(IEvent @event)
        {
            var _event = @event as GrainEvent;

            var @eventId = _event.GrainId.ToString() + _event.Version;

            var tcs = new TaskCompletionSource<IOperationResult>();

#pragma warning disable 4014
            Task.Factory.StartNew(() =>
#pragma warning restore 4014
            {
                try
                {
                    var result = _bucket.Insert(@eventId, JsonConvert.SerializeObject(@event));
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(new Exception("append event to store exception", ex));
                }
            });

            var opResult = await tcs.Task;

            if (!opResult.Success)
                throw new Exception("append event to store exception", opResult.Exception);
        }

        private IEvent ConvertJsonToEvent(string eventJson)
        {
            dynamic @event = JsonConvert.DeserializeObject(eventJson);
            uint eventTypeCode = @event.TypeCode;
            Type eventType;

            if (!EventNameTypeMapping.TryGetEventType(eventTypeCode, out eventType))
            {
                throw new Exception("unknow event type");
            }

            var convertEvent = JsonConvert.DeserializeObject(eventJson, eventType) as IEvent;

            return convertEvent;
        }
    }
}
