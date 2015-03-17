using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Views;
using Couchbase.Configuration;
using Couchbase.Configuration.Client;
using System.Dynamic;
using Couchbase.Core;
using System.Collections.Concurrent;
using System.Threading;
using Couchbase.Management;

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

        public async Task<IEnumerable<string>> ReadFrom(string grainId, ulong eventId = 0)
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

            var result = new List<string>();

            var indexs = await _bucket.QueryAsync<dynamic>(query);

            var keys = indexs.Rows.Select(r => r.Id).ToList();

            if (keys.Count > 0)
            {
                var events = _bucket.Get<string>(keys);

                if (events.Count(e => e.Value.Success) == keys.Count)
                {
                    result = events.Select(et => et.Value.Value).ToList();
                }
                else
                    throw new Exception("read from event store exception", events.Select(et => et.Value.Exception).FirstOrDefault());
            }

            return result;
        }

        public Task Append(string grainId, ulong eventVersion, string eventJsonString)
        {
            var @eventId = grainId.ToString() + eventVersion;

            var tcs = new TaskCompletionSource<IOperationResult>();

            WaitCallback write = (state) =>
            {
                var result = _bucket.Insert(grainId + eventVersion.ToString(), eventJsonString);
                tcs.SetResult(result);
            };

            ThreadPool.QueueUserWorkItem(write, null);

            var opResult = tcs.Task.Result;

            if (!opResult.Success)
            {
                throw new Exception("append event to store exception", opResult.Exception);
            }
            return TaskDone.Done;
        }
    }
}
