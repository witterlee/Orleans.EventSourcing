using Orleans.Providers;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public static class SiloHostExtension
    {
        public static SiloHost UseEventStore(this SiloHost siloHost, string rabbitMqConnectString, int eventMessageProcessorPoolSize = 10)
        {
            //give the rabbitmq connect string
            var setting = new EventStoreDispatcherSetting(rabbitMqConnectString, eventMessageProcessorPoolSize);
            EventStoreDispatcher.Initialize(setting);

            return siloHost;
        }
    }
}
