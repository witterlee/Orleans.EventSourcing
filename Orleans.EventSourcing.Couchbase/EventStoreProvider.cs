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

namespace Orleans.EventSourcing.Couchbase
{
    public class EventStoreProvider : IEventStoreProvider
    {
        private static IBucket bucket;
        private static string bucketName;
        private static string bucketPwd;
        private static Cluster cluster;
        private static object locker = new object();
        private static dynamic _setting;
        private Cluster Cluster
        {
            get
            {
                if (cluster == null)
                {
                    lock (locker)
                    {
                        if (cluster == null)
                        {
                            var servers = _setting.servers;
                            var uris = new List<Uri>();

                            bucketName = _setting.bucket.name;
                            bucketPwd = _setting.bucket.password;

                            dynamic pool = _setting.bucket.connectionPool;
                            //"usessl": false, "maxsize": 10, "minsize":5
                            bool usessl = pool.usessl;
                            int maxsize = (int)pool.maxsize;
                            int minsize = (int)pool.minsize;

                            foreach (var uri in servers)
                            {
                                uris.Add(new Uri(uri));
                            }

                            var config = new ClientConfiguration()
                            {
                                Servers = uris,
                                PoolConfiguration = new PoolConfiguration { MaxSize = maxsize, UseSsl = usessl, MinSize = minsize }
                            };
                            cluster = new Cluster(config);
                        }
                    }
                }

                return cluster;
            }
        }

        private IBucket Bucket
        {
            get
            {
                if (bucket == null)
                {
                    lock (locker)
                    {
                        if (bucket == null)
                            bucket = Cluster.OpenBucket(bucketName, bucketPwd);
                    }
                }
                return bucket;
            }
        }
        public Task Initialize(ExpandoObject settings)
        {
            _setting = settings;

            return Task.Run(() => { });
        }

        public IEventStore Create() 
        {
            string user = _setting.administrator;
            string pwd = _setting.adminpassword;
            return new CouchbaseEventStore(this.Bucket, user, pwd);
        }
    }
}
