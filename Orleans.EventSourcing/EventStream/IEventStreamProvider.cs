using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public interface IEventStreamProvider
    {
        Task<bool> PublishAsync(IEvent @event);
    }

}
