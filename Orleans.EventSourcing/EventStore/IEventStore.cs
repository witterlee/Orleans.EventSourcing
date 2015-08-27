using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public interface IEventStore
    {
        Task<EventWriteResult> AppendAsync(IEvent @event);
        Task<IEnumerable<IEvent>> ReadFromAsync(string grainUniqueId, long eventVersion = 0);
        Task<IEvent> ReadOneAsync(string grainUniqueId, string commandId);
    }

    public enum EventWriteResult
    {
        Success = 1,
        Duplicate = 2,
        UnknowError = 3
    }
}
