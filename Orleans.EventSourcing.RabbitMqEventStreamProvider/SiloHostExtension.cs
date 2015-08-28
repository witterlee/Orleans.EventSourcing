using System;
using Orleans.Runtime.Host;

namespace Orleans.EventSourcing.RabbitMqEventStreamProvider
{
    public static class SiloHostExtension
    { 
        public static SiloHost UseRabbitMqEventStreamProvider(this SiloHost siloHost, IEventStreamProviderFactory eventStreamProviderFactory)
        {
            if (eventStreamProviderFactory == null)
                throw new ArgumentNullException("eventStreamProviderFactory", "eventStreamProviderFactory is null");

            EventStreamProviderManager.Initailize(eventStreamProviderFactory);

            return siloHost;
        } 
    }
}
