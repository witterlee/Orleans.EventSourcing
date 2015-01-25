using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public interface IEventStore
    {
        Task Append(string eventId, string eventJsonString);
        Task<IEnumerable<string>> ReadFrom(Guid grainId, ulong eventVersion = 0);
    }
}
