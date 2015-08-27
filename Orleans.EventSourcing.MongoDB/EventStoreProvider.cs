using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Orleans.EventSourcing.MongoDB
{
    public class EventStoreProvider : IEventStoreProvider
    {
        private IMongoDatabase _database;

        public Task Initialize(EventStoreProviderSetting settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ConnectionString)) throw new ArgumentException("ConnectionString property not set");
            if (string.IsNullOrWhiteSpace(settings.DatabaseName)) throw new ArgumentException("Database property not set");

            MongoClient client = new MongoClient(settings.ConnectionString);
          
            _database = client.GetDatabase(settings.DatabaseName); 
            return TaskDone.Done;
        }

        public Task<IEventStore> Create<T>() where T : IEventSourcingGrain
        {
            return Task.FromResult(new MongoDbEventStore(this._database) as IEventStore);
        }
    }
}
