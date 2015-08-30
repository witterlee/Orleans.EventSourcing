namespace Orleans.EventSourcing.QuerySide
{
    public interface IQuerySideHandler<in T> : IQuerySideHandler where T : GrainEvent
    {
        WriteResult Handle(T @event);
    }
    public interface IQuerySideHandler { }


    public enum WriteResult
    {
        Success = 1,
        Ignore = 2,
        Wait = 3
    }
}
