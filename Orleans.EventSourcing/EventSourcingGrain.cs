using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using Orleans.Providers;
using Orleans.Concurrency;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Linq;
using System.Collections.Concurrent;

namespace Orleans.EventSourcing
{

    public abstract class EventSourcingGrain<TGrain, TState> : Grain<TState>, IEventSourcingGrain<TState>
        where TGrain : class ,IEventSourcingGrain<TState>, IGrain
        where TState : class,IEventSourcingState
    {
        private const int APPLY_EVENT_ERROR = 60101;
        private EventStore<TGrain, TState> eventStore { get; set; }

        /// <summary>
        /// apply event to grain, if applied success retrun SuccessMessage,else return ErrorMessage
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected async Task ApplyEvent(GrainEvent @event)
        {
            try
            {
                int typeCode;

                if (!EventNameTypeMapping.TryGetEventTypeCode(@event.GetType(), out typeCode))
                    throw new Exception("unknow event type");

                @event.GrainId = this.GetGrainId();
                @event.Version = this.GetState().Version + 1;
                @event.UTCTimestamp = DateTime.Now.ToUniversalTime();
                @event.TypeCode = typeCode;
                await this.eventStore.WriteEvent(@event);
            }
            catch (Exception ex)
            {
                var log = this.GetLogger("event_store");
                log.Warn(APPLY_EVENT_ERROR, string.Format("applay event {0} error, eventId={1}", @event.GetType().FullName, @event.Version), ex);
                throw ex;
            }
        }

        public async override Task OnActivateAsync()
        {
            this.eventStore = await EventStore<TGrain, TState>.Initialize(this as TGrain, 100);
            await this.eventStore.ReplayEvents();
            await base.OnActivateAsync();
        }

        public TState GetState()
        {
            return this.State;
        }

        public string GetGrainId()
        {
            string extKey;
            var grainId = this.GetPrimaryKey(out extKey).ToString();
            var currentEventId = this.State.Version;
            grainId = grainId + (string.IsNullOrEmpty(extKey) ? string.Empty : "_" + extKey);

            return grainId;
        }
    }
}
