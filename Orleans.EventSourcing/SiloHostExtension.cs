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
            EventStoreDispatcherGrain.Initialize(setting);

            //var BOOTSTARTP_PROVIDER_CONFIGS_NAME = "BootstrapProviders";
            //ProviderCategoryConfiguration providerConfiguration;
            //if (!siloHost.Config.Globals.ProviderConfigurations.TryGetValue(BOOTSTARTP_PROVIDER_CONFIGS_NAME, out providerConfiguration))
            //{
            //    providerConfiguration = new ProviderCategoryConfiguration() { Name = BOOTSTARTP_PROVIDER_CONFIGS_NAME };
            //}

            //var eventStoreProvider = new ProviderConfiguration(new Dictionary<string, string>(), typeof(EventStoreInitBootstrapProvider).FullName, "EventStoreInitBootstrapProvider");
            //providerConfiguration.Providers.Add("EventStoreInitBootstrapProvider", eventStoreProvider);

            return siloHost;
        }
    }

    public class EventStoreInitBootstrapProvider : IBootstrapProvider
    { 
        public Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            //process uncommit event message(store uncommit event message to event-store)
            //var eventDispatcher = GrainFactory.GetGrain<IEventStoreDispatcher>(0);

            return TaskDone.Done;
        }

        public string Name
        {
            get { return "EventStoreInitBootstrapProvider"; }
        }
    }
}
