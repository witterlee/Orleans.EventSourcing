using System;

namespace Orleans.EventSourcing
{
    [Serializable]
    public class EventStoreProviderEmptyException : Exception
    { 
        public EventStoreProviderEmptyException(Type grainType)
        {
            var message = grainType.FullName + " has not EventStoreProviderAttribute, you can set a event store provider as default or get a EventStoreProviderAttribute to this grain type.";
        }
    }

}
