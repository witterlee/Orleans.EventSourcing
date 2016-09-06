using System;
using System.Threading.Tasks;
using Orleans.Runtime;

namespace Orleans.EventSourcing
{

    public abstract class EventSourcingGrain<TGrain, TState> : Grain<TState>, IEventSourcingGrain<TState>
        where TGrain : Grain<TState>, IEventSourcingGrain<TState>, IGrain
        where TState : EventSourcingState
    {
        private const int APPLY_EVENT_ERROR = 100000 + 1;
        private EventStore<TGrain, TState> EventStore { get; set; }

        /// <summary>
        /// apply event to grain, if applied success retrun SuccessMessage,else return ErrorMessage
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected Task RaiseEvent(GrainEvent @event)
        {
            try
            {
                int typeCode;

                if (!EventNameCodeMapping.TryGetEventTypeCode(@event.GetType(), out typeCode))
                    throw new Exception("unknow event type");

                @event.GrainId = this.GetGrainId();
                @event.Version = this.GetState().Version + 1;
                @event.UtcTimestamp = DateTime.Now.ToUniversalTime();
                @event.TypeCode = typeCode;

                return this.EventStore.WriteEvent(@event);
            }
            catch (Exception ex)
            {
                var log = this.GetLogger("event_store");
                log.Warn(APPLY_EVENT_ERROR, $"applay event {@event.GetType().FullName} error, eventId={@event.Version}", ex);
                throw ex;
            }
        }

        public override async Task OnActivateAsync()
        {
            this.EventStore = await EventStore<TGrain, TState>.Initialize(this as TGrain, 100);
            await this.EventStore.ReplayEvents();
            await base.OnActivateAsync();
        }

        public TState GetState()
        {
            return this.State;
        }

        public Task WriteSnapshotAsync()
        {
            return this.WriteStateAsync();
        }

        public string GetGrainId()
        {
            string rtnKey;
            string extKey;
            var guidId = this.GetPrimaryKey(out extKey);

            if (guidId == Guid.Empty)
            {
                rtnKey = this.IdentityString;
            }
            else
            {
                rtnKey = guidId.ToString();
            }
            return rtnKey;
        }
    }
}
