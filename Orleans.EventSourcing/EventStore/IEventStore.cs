using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public interface IEventStore
    {
        Task Append(IEvent @event);
        Task<IEnumerable<IEvent>> ReadFrom(string grainUniqueId, ulong eventVersion = 0);
    }
}
