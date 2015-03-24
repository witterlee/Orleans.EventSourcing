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
    public class EventStoreSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsDefaultCollection = true)]

        [ConfigurationCollection(typeof(EventStoreProviderSettingCollection), AddItemName = "provider")]
        public EventStoreProviderSettingCollection Providers
        {
            get
            {
                return (EventStoreProviderSettingCollection)base[""];
            }
        } 
    }

    public class EventStoreProviderSettingCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new EventStoreProviderSetting();
        }
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((EventStoreProviderSetting)element).Name;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }
        protected override string ElementName
        {
            get
            {
                return "provider";
            }
        }

        public EventStoreProviderSetting this[int index]
        {
            get
            {
                return (EventStoreProviderSetting)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }


    public class EventStoreProviderSetting : ConfigurationElement
    {
        [ConfigurationProperty("Name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["Name"]; }
            set { base["Name"] = value; }

        }
        [ConfigurationProperty("Type", IsRequired = true)]
        public string Type
        {
            get { return (string)base["Type"]; }
            set { base["Type"] = value; }
        }

        [ConfigurationProperty("Default", IsRequired = false, DefaultValue = false)]
        public bool Default
        {
            get { return (bool)base["Default"]; }
            set { base["Default"] = value; }
        }

        [ConfigurationProperty("ConnectionString", IsRequired = false)]
        public string ConnectionString
        {
            get { return (string)base["ConnectionString"]; }
            set { base["ConnectionString"] = value; }
        }

        [ConfigurationProperty("ConfigSection", IsRequired = false)]
        public string ConfigSection
        {
            get { return (string)base["ConfigSection"]; }
            set { base["ConfigSection"] = value; }
        }

        [ConfigurationProperty("DatabaseName", IsRequired = false)]
        public string DatabaseName
        {
            get { return (string)base["DatabaseName"]; }
            set { base["DatabaseName"] = value; }
        }
         
    }


}
