namespace Orleans.EventSourcing
{
    public class EventSourcingState : GrainState
    {
        public long Version { get; set; }
    }
}
