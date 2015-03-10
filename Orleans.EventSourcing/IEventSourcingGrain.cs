using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;

namespace Orleans.EventSourcing
{
    /// <summary>
    /// Orleans grain communication interface IEventSourcingGrain
    /// </summary>
    public interface IEventSourcingGrain<TState> : IEventSourcingGrain
        where TState : IEventSourcingState
    {
        TState GetState();
        string GetGrainId();
    }

    public interface IEventSourcingGrain : Orleans.IGrain { }
}
