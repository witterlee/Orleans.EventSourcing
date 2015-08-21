using System;

namespace Orleans.EventSourcing
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EventStoreProviderAttribute : Attribute
    {
        public string ProviderName { get; set; }
    }
}
