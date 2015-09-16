using RabbitMQ.Client;

namespace Orleans.EventSourcing.RabbitMqEventStreamProvider
{
    public class EventStreamProviderFactory : IEventStreamProviderFactory
    {
        public EventStreamProviderFactory(IConnectionFactory connectionFactory, string deployId,int queueCount)
        {
            EventStreamProvider.SetConnectionFactory(connectionFactory, deployId, queueCount);
        }


        public IEventStreamProvider CreateEventStreamProvider()
        {
            return new EventStreamProvider();
        }
    }
}
