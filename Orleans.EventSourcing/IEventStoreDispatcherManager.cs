using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;
using System.Linq;

namespace Orleans.EventSourcing
{
    public interface IEventStoreDispatcherManager : Orleans.IGrainWithIntegerKey
    {
         
    }
}
