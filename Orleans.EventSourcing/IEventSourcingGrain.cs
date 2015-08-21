namespace Orleans.EventSourcing
{
    /// <summary>
    /// Orleans grain communication interface IEventSourcingGrain
    /// </summary>
    public interface IEventSourcingGrain<TState> : IEventSourcingGrain
        where TState : EventSourcingState
    {
        TState GetState();
        string GetGrainId();
    }

    public interface IEventSourcingGrain : IGrain { }
}
