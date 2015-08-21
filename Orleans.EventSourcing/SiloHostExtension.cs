using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Orleans.Runtime.Host;

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

        public static SiloHost RegisterGrain(this SiloHost siloHost, IDictionary<string, int> typeNameCodeMapping, params Assembly[] assemlies)
        {
            if (assemlies != null && assemlies.Any())
            {
                GrainInternalEventHandlerProvider.RegisterInternalEventHandler(assemlies); 

                if (typeNameCodeMapping.Any())
                {
                    var types = assemlies.SelectMany(assemly => assemly.GetTypes());
                    foreach (var kv in typeNameCodeMapping)
                    {
                        var type = types.Single(t => t.FullName == kv.Key);

                        EventNameTypeMapping.RegisterEventType(kv.Value, type); 
                    }
                }
            }
            return siloHost;
        }
    }
}
