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
        public IEnumerable<EventStoreProviderConfig> ProviderConfigs { get { return _providerConfigs; } }

        public EventStoreProviderConfigManager(string configFileName = "eventstore.json")
        {
            try
            {
                var json = File.ReadAllText(configFileName);
                var _providerConfigCollection = JsonConvert.DeserializeObject<EventStoreProviderConfigCollection>(json);

                if (_providerConfigCollection != null)
                {
                    _providerConfigs = _providerConfigCollection.Providers.ToList();  
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

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("settings")]
        public ExpandoObject Settings { get; set; }
    }


}
