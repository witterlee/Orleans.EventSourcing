using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Providers;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using System.Dynamic;
using Newtonsoft.Json;

namespace Orleans.EventSourcing
{
    public class EventStoreProviderConfigManager
    {
        private List<EventStoreProviderConfig> _providerConfigs = new List<EventStoreProviderConfig>();
        private string _defaultProviderName = string.Empty;
        public bool HasDefaultProvider { get { return !string.IsNullOrEmpty(_defaultProviderName); } }  
        public string DefaultProviderName { get { return _defaultProviderName; } }
        public IEnumerable<EventStoreProviderConfig> ProviderConfigs { get { return _providerConfigs; } }

        public EventStoreProviderConfigManager(string configFileName = "eventstore.json")
        {
            var json = File.ReadAllText(configFileName);
            try
            {
                var _providerConfigCollection = JsonConvert.DeserializeObject<EventStoreProviderConfigCollection>(json);

                if (_providerConfigCollection != null)
                {
                    _providerConfigs = _providerConfigCollection.Providers.ToList();
                    var _default = _providerConfigs.SingleOrDefault(p => p.Default);
                    _defaultProviderName = _default == null ? string.Empty : _default.Name;
                }
            }
            catch (Exception ex)
            {
                throw new ConfigInvalidException("event store config invalid", ex);
            }

        }
    }


    public class EventStoreProviderConfigCollection
    {
        [JsonProperty("providers")]
        public IEnumerable<EventStoreProviderConfig> Providers { get; set; }
    }

    public class EventStoreProviderConfig
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("default")]
        public bool Default { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("settings")]
        public ExpandoObject Settings { get; set; }
    }


}
