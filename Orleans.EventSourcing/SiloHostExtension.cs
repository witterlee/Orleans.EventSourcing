using Orleans.Providers;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Reflection;

namespace Orleans.EventSourcing
{
    public static class SiloHostExtension
    {
        public static SiloHost UseEventStore(this SiloHost siloHost, EventStoreSection eventStoreSection)
        {
            if (eventStoreSection == null)
                throw new ArgumentNullException("eventStoreSection", "eventStoreSection is null");

            EventStoreProviderManager.Initailize(eventStoreSection);

            return siloHost;
        }

        public static SiloHost UseEventStore(this SiloHost siloHost, string eventStoreSectionName)
        {
            if (string.IsNullOrEmpty(eventStoreSectionName))
                throw new ArgumentNullException("eventStoreSectionName", "eventStoreSectionName is null");

            var eventStoreSection = (EventStoreSection)ConfigurationManager.GetSection("eventStoreProvider");
            siloHost.UseEventStore(eventStoreSection);

            return siloHost;
        }

        public static SiloHost RegisterGrain(this SiloHost siloHost, params Assembly[] assemlies)
        {
            if (assemlies != null && assemlies.Count() > 0)
            {
                GrainInternalEventHandlerProvider.RegisterInternalEventHandler(assemlies);
                EventNameTypeMapping.RegisterEventType(assemlies);
            }
            return siloHost;
        }
    }
}
