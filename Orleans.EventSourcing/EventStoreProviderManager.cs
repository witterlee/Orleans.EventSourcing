using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Providers;
using System.Collections.Concurrent;
using System.Reflection;

namespace Orleans.EventSourcing
{
    public class EventStoreProviderManager
    {
        private static EventStoreProviderConfigManager configManager = new EventStoreProviderConfigManager();
        private static ConcurrentDictionary<Type, string> mappings = new ConcurrentDictionary<Type, string>();
        private static Dictionary<string, IEventStoreProvider> providers = new Dictionary<string, IEventStoreProvider>();

        static EventStoreProviderManager()
        {
            foreach (var config in configManager.ProviderConfigs)
            {
                var providerType = Type.GetType(config.Type, true);
                var provider = (IEventStoreProvider)Activator.CreateInstance(providerType);
                provider.Initialize(config.Settings);
                providers.Add(config.Name, provider);
            }
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

            if (attr == null && !configManager.HasDefaultProvider)
            {
                throw new EventStoreProviderEmptyException(grainType);
            }
            return attr == null ? configManager.DefaultProviderName : attr.ProviderName;
        }

    }
}
