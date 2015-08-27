using RabbitMQ.Client;

namespace Orleans.EventSourcing.RabbitMqEventStreamProvider
{
    public class EventStreamProviderFactory : IEventStreamProviderFactory
    {
        public EventStreamProviderFactory(IConnectionFactory connectionFactory, int queueCount)
        {
            EventStreamProvider.SetConnectionFactory(connectionFactory, queueCount);
        }


        public IEventStreamProvider CreateEventStreamProvider()
        {
            return new EventStreamProvider();
        }
    }
}
