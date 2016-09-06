using System;
using Orleans.Runtime;

namespace Orleans.EventSourcing
{
    [Serializable]
    public class GrainEvent : IEvent
    {
        public GrainEvent()
        {
            var cmdId = RequestContext.Get("CommandId");
            this.CommandId = cmdId?.ToString() ?? "";
        }
        public string GrainId { get; set; }
        public string CommandId { get; set; }
        public DateTime UtcTimestamp { get; set; }
        public int TypeCode { get; set; }
        public int Version { get; set; }

        public virtual void Apply<T>(T @event)
        { 
            throw new NotImplementedException();
        }
    }
    /// <summary>
    /// The event interface.
    /// </summary> 
    public interface IEvent
    {
        string GrainId { get; }
        string CommandId { get; }
        int Version { get; }
        int TypeCode { get; }
        DateTime UtcTimestamp { get; }

        void Apply<T>(T state);
    }
}
