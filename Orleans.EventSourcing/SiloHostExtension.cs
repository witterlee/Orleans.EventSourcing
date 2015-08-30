using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Orleans.EventSourcing.QuerySide;
using Orleans.Runtime.Host;

namespace Orleans.EventSourcing
{
    public static class SiloHostExtension
    {
        public static SiloHost UseEventStore(this SiloHost siloHost, EventStoreSection eventStoreSection, IDictionary<string, int> typeNameCodeMapping, params Assembly[] assemlies)
        {
            if (eventStoreSection == null)
                throw new ArgumentNullException("eventStoreSection", "eventStoreSection is null");
            RegisterGrain(siloHost, typeNameCodeMapping, assemlies);
            EventStoreProviderManager.Initailize(eventStoreSection);

            return siloHost;
        }

        public static SiloHost UseEventStore(this SiloHost siloHost, string eventStoreSectionName, IDictionary<string, int> typeNameCodeMapping, params Assembly[] assemlies)
        {
            if (string.IsNullOrEmpty(eventStoreSectionName))
                throw new ArgumentNullException("eventStoreSectionName", "eventStoreSectionName is null");

            var eventStoreSection = (EventStoreSection)ConfigurationManager.GetSection("eventStoreProvider");
            siloHost.UseEventStore(eventStoreSection, typeNameCodeMapping, assemlies);

            return siloHost;
        }

        public static SiloHost UseQuerySideBus(this SiloHost siloHost, params Assembly[] assemlies)
        {
            if (assemlies == null || !assemlies.Any())
                throw new ArgumentNullException("assemlies", "assemlies is null");

            var queryHandlerContainer = new QuerySideProcessorContainer();
            queryHandlerContainer.RegisterHandlers(assemlies);

            QuerySideProcessorBus.Initialize(queryHandlerContainer);

            return siloHost;
        }
        private static SiloHost RegisterGrain(this SiloHost siloHost, IDictionary<string, int> typeNameCodeMapping, params Assembly[] assemlies)
        {
            if (assemlies != null && assemlies.Any())
            {
                if (typeNameCodeMapping.Any())
                {
                    var types = assemlies.SelectMany(assemly => assemly.GetTypes());
                    foreach (var kv in typeNameCodeMapping)
                    {
                        var type = types.Single(t => t.FullName == kv.Key);

                        EventNameCodeMapping.RegisterEventType(kv.Value, type);
                    }
                }
                GrainInternalEventHandlerProvider.RegisterInternalEventHandler(assemlies);
            }
            return siloHost;
        }
    }
}
