using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Orleans.Providers;
using System.Collections.Concurrent;
using System.Reflection;

namespace Orleans.EventSourcing
{
    public class EventStoreProviderManager
    {
        private static Dictionary<string, IEventStoreProvider> providers;
        private static ConcurrentDictionary<Type, string> mappings = new ConcurrentDictionary<Type, string>();
        private static bool HasDefaultProvider = false;
        private static string DefaultProviderName = string.Empty;

        public static void Initailize(EventStoreSection configSection)
        {
            if (configSection == null)
                throw new ArgumentNullException("event store config section null");

            if (configSection.Providers != null && configSection.Providers.Count > 0)
            {
                providers = new Dictionary<string, IEventStoreProvider>();

                foreach (EventStoreProviderSetting providerSetting in configSection.Providers)
                {
                    var providerType = Type.GetType(providerSetting.Type, true);
                    var provider = (IEventStoreProvider)Activator.CreateInstance(providerType);
                    provider.Initialize(providerSetting);
                    providers.Add(providerSetting.Name, provider);

                    if (providerSetting.Default == true)
                    {
                        DefaultProviderName = providerSetting.Name;
                        HasDefaultProvider = true;
                    }
                } 
            }
            else
                throw new ArgumentNullException("no event store provider in config");
        }
        public static IEventStoreProvider GetProvider<T>() where T : IEventSourcingGrain
        {
            return GetProvider(typeof(T));
        }

        public static IEventStoreProvider GetProvider(Type grainType)
        {
            IEventStoreProvider provider;
            var providerName = mappings.GetOrAdd(grainType, GetEventStoreProviderName);

            providers.TryGetValue(providerName, out provider);

            return provider;
        }

        private static string GetEventStoreProviderName<T>()
        {
            return GetEventStoreProviderName(typeof(T));
        }

        private static string GetEventStoreProviderName(Type grainType)
        {
            var attr = grainType.GetCustomAttribute<EventStoreProviderAttribute>();

            if (attr == null && !HasDefaultProvider)
            {
                throw new EventStoreProviderEmptyException(grainType);
            }
            return attr == null ? DefaultProviderName : attr.ProviderName;
        }

    }
}
