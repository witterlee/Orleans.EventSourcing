using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;
using System.Linq;

namespace Orleans.EventSourcing.Interface
{      
    public interface IEventStoreDispatcher : Orleans.IGrainWithIntegerKey
    {
        Task Append(Guid grainId, ulong eventVersion, IEvent @event);
        Task<IOrderedEnumerable<IEvent>> ReadFrom(Guid grainId, ulong eventVersion = 0);
    }
}
