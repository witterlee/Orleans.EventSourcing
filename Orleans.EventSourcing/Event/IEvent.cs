using System;
using Orleans.Runtime;

namespace Orleans.EventSourcing
{

    public class GrainEvent : IEvent
    {
        public GrainEvent()
        {
            var cmdId = RequestContext.Get("CommandId");
            this.CommandId = cmdId == null ? "" : cmdId.ToString();
        }
        public string GrainId { get; set; }
        public string CommandId { get; set; }
        public DateTime UTCTimestamp { get; set; }
        public int TypeCode { get; set; }
        public long Version { get; set; }
    }

    /// <summary>
    /// The event interface.
    /// </summary>
    public interface IEvent
    {
        string GrainId { get; }
        string CommandId { get; }
        long Version { get; }
        int TypeCode { get; }
        DateTime UTCTimestamp { get; }
    }
}
