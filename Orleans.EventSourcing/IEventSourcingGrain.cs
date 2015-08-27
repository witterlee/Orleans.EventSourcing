using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    /// <summary>
    /// Orleans grain communication interface IEventSourcingGrain
    /// </summary>
    public interface IEventSourcingGrain<out TState> : IEventSourcingGrain
        where TState : EventSourcingState
    {
        TState GetState();
        Task WriteSnapshot();
        string GetGrainId();
    }

    public interface IEventSourcingGrain : IGrain { }
}
