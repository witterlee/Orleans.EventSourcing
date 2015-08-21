using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public interface IEventStore
    {
        Task Append(IEvent @event);
        Task<IEnumerable<IEvent>> ReadFrom(string grainUniqueId, long eventVersion = 0);
    }
}
