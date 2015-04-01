using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{

    public class GrainEvent : IEvent
    {
        public string GrainId { get; set; }
        public ulong Version { get; set; }
        public DateTime UTCTimestamp { get; set; }
        public uint TypeCode { get; set; }
    }

    /// <summary>
    /// The event interface.
    /// </summary>
    public interface IEvent
    {
        string GrainId { get; }
        ulong Version { get; }
        uint TypeCode { get; }
        DateTime UTCTimestamp { get; }
    }
}
