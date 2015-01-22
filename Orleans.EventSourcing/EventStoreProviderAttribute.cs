using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Providers;

namespace Orleans.EventSourcing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class EventStoreProviderAttribute : Attribute
    {
        public EventStoreProviderAttribute(string providerName)
        {
            this.ProviderName = providerName;
        }
        public string ProviderName { get; set; }
    }
}
