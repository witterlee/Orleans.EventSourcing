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

        public static IEventStoreProvider GetProvider()
        { 
            if (providers.Count == 0)
            {
                throw new EventStoreProviderEmptyException();
            }
            else
            {
                return providers.First().Value;
            } 
        }
    }
}
