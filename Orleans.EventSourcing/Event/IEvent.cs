using System;

namespace Orleans.EventSourcing
{

    public class GrainEvent : IEvent
    {
        public string GrainId { get; set; }
        public long Version { get; set; }
        public DateTime UTCTimestamp { get; set; }
        public int TypeCode { get; set; }
    }

    /// <summary>
    /// The event interface.
    /// </summary>
    public interface IEvent
    {
        string GrainId { get; }
        long Version { get; }
        int TypeCode { get; }
        DateTime UTCTimestamp { get; }
    }
}
