using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{

    public class GrainEvent : IEvent
    {
        public Guid GrainId { get; set; }
        public ulong Version { get; set; }
        public DateTime UTCTimestamp { get; set; }
        public string Type { get { return this.GetType().FullName; } }
    }

    /// <summary>
    /// The event interface.
    /// </summary>
    public interface IEvent
    {
        Guid GrainId { get; }
        ulong Version { get; }
        string Type { get; }
        DateTime UTCTimestamp { get; }
    }
}
