using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public interface IEventStreamProviderFactory
    {
        IEventStreamProvider CreateEventStreamProvider();
    }

}
