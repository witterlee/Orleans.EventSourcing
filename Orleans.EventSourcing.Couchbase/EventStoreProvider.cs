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
using System.Configuration;
using System.Threading;
using Couchbase.Configuration.Client.Providers;

namespace Orleans.EventSourcing.Couchbase
{
    public class EventStoreProvider : IEventStoreProvider
    {
        private static IBucket bucket;
        private static Cluster cluster;
        private static ClientConfiguration config;
        private static bool initialized;
        private static object locker = new object();
        private Cluster Cluster
        {
            get { return cluster; }
        }

        private async Task<IBucket> GetBucket()
        {
            var tcs = new TaskCompletionSource<IBucket>();
            if (bucket == null)
            {
#pragma warning disable 4014
                Task.Factory.StartNew(() =>
#pragma warning restore 4014
                {
                    try
                    {
                        lock (locker)
                        {
                            if (bucket == null)
                            {
                                var bucketConfig = config.BucketConfigs.First();
                                bucket = Cluster.OpenBucket(bucketConfig.Value.BucketName, bucketConfig.Value.Password);
                            }
                        }

                        tcs.SetResult(bucket);
                    }
                    catch (Exception ex)
                    {
                        tcs.SetException(new Exception("Couchbase Event Store Init Bucket Exception", ex));
                    }
                });

                await tcs.Task;
            }

            return bucket;
        }
        public Task Initialize(EventStoreProviderSetting settings)
        {
            if (initialized) throw new Exception("Event store provider has initialized,do not initialize again.");


            var section = (CouchbaseClientSection)ConfigurationManager.GetSection(settings.ConfigSection);
            if (section.Servers.Count == 0) throw new ArgumentException("Couchbase servers not set");

            config = new ClientConfiguration(section);
            cluster = new Cluster(config);
            initialized = true;
            return TaskDone.Done;
        }

        public async Task<IEventStore> Create<T>() where T : IEventSourcingGrain
        {
            var _bucket = await this.GetBucket();

            return new CouchbaseEventStore(_bucket);
        }
    }
}
