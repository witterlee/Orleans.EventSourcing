using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public interface IEventStore
    {
        Task Append(string grainUniqueId, ulong eventVersion, string eventJsonString);
        Task<IEnumerable<string>> ReadFrom(string grainUniqueId, ulong eventVersion = 0);
    }
}
