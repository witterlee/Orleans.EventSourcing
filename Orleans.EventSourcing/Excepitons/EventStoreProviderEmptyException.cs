using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Concurrency;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

namespace Orleans.EventSourcing
{
    public class EventStoreProviderEmptyException : Exception
    { 
        public EventStoreProviderEmptyException(Type grainType)
        {
            var message = grainType.FullName + " has not EventStoreProviderAttribute, you can set a event store provider as default or get a EventStoreProviderAttribute to this grain type.";
        }
    }

}
