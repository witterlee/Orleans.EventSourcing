namespace Orleans.EventSourcing
{
    public interface IEventStreamProviderFactory
    {
        IEventStreamProvider CreateEventStreamProvider();
    }

}
