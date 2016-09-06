using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Orleans.EventSourcing
{
    public class EventStoreProviderManager
    {
        private static Dictionary<string, IEventStoreProvider> _providers;
        private static readonly ConcurrentDictionary<Type, string> Mappings = new ConcurrentDictionary<Type, string>();
        private static bool _hasDefaultProvider;
        private static string _defaultProviderName = string.Empty;

        public static void Initailize(EventStoreSection configSection)
        {
            if (configSection == null)
                throw new ArgumentNullException(nameof(configSection), "event store config section null");

            if (configSection.Providers != null && configSection.Providers.Count > 0)
            {
                _providers = new Dictionary<string, IEventStoreProvider>();

                foreach (EventStoreProviderSetting providerSetting in configSection.Providers)
                {
                    var providerType = Type.GetType(providerSetting.Type, true);
                    var provider = (IEventStoreProvider)Activator.CreateInstance(providerType);
                    provider.Initialize(providerSetting);
                    _providers.Add(providerSetting.Name, provider);

                    if (providerSetting.Default)
                    {
                        _defaultProviderName = providerSetting.Name;
                        _hasDefaultProvider = true;
                    }
                }
            }
            else
                throw new ArgumentNullException(nameof(configSection), "no event store provider in config");
        }
        public static IEventStoreProvider GetProvider<T>() where T : IEventSourcingGrain
        {
            return GetProvider(typeof(T));
        }

        public static IEventStoreProvider GetProvider(Type grainType)
        {
            IEventStoreProvider provider;
            var providerName = Mappings.GetOrAdd(grainType, GetEventStoreProviderName);

            _providers.TryGetValue(providerName, out provider);

            return provider;
        }

        private static string GetEventStoreProviderName<T>()
        {
            return GetEventStoreProviderName(typeof(T));
        }

        private static string GetEventStoreProviderName(Type grainType)
        {
            var attr = grainType.GetCustomAttribute<EventStoreProviderAttribute>();

            if (attr == null && !_hasDefaultProvider)
            {
                throw new EventStoreProviderEmptyException(grainType);
            }
            return attr == null ? _defaultProviderName : attr.ProviderName;
        }

    }
}
