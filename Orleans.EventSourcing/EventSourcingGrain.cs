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
        where TGrain : Grain<TState>, IEventSourcingGrain<TState>, IGrainWithGuidKey
        where TState : class,IEventSourcingState
    {
        private static ConcurrentBag<string> registerAssembly = new ConcurrentBag<string>();
        private static object lockObj = new object();
        public EventSourcingGrain()
        {
            var assembly = this.GetType().Assembly;
            if (!registerAssembly.Contains(assembly.FullName))
            {
                lock (lockObj)
                {
                    if (!registerAssembly.Contains(assembly.FullName))
                    {
                        GrainInternalEventHandlerProvider.RegisterInternalEventHandler(assembly);
                        EventNameTypeMapping.RegisterEventType(assembly);
                        registerAssembly.Add(assembly.FullName);
                    }
                }
            }
        }
        private const int APPLY_EVENT_ERROR = 60001;
        private EventStore<TGrain, TState> eventStore { get; set; }

        /// <summary>
        /// apply event to grain, if applied success retrun SuccessMessage,else return ErrorMessage
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        protected async Task ApplyEvent(GrainEvent @event)
        {
            //try
            //{
            @event.GrainId = this.GetPrimaryKey();
            @event.Version = this.GetState().Version + 1;
            @event.UTCTimestamp = DateTime.Now.ToUniversalTime();
            this.HandleEvent(@event);
            await this.eventStore.WriteEvent(@event);
            //return SuccessMessage.Instance;
            //}
            //catch (Exception ex)
            //{
            //    var log = this.GetLogger("event_store");
            //    log.Error(APPLY_EVENT_ERROR, string.Format("applay event {0} error, eventId={1}", @event.GetType().FullName, @event.Version), ex);

            //    return new ErrorMessage(ex.Message);
            //}
        }

        protected ulong GetNextEventId()
        {
            return this.State.Version + 1;
        }

        public async override Task ActivateAsync()
        {
            this.eventStore = new EventStore<TGrain, TState>(this as TGrain, 100);
            await this.eventStore.ReplayEvents();
            await base.ActivateAsync();
        }

        public TState GetState()
        {
            return this.State;
        }
        private void HandleEvent(IEvent @event)
        {
            var eventHandler = GrainInternalEventHandlerProvider.GetInternalEventHandler(this.GetType(), @event.GetType());

            if (eventHandler == null)
            {
                throw new Exception(string.Format("Could not find event handler for [{0}] of [{1}]", @event.GetType().FullName, this.GetType().FullName));
            }
            eventHandler.Invoke(this, @event);

            if (this.State.Version + 1 != @event.Version)
            {
                throw new Exception(string.Format("inlidate event apply,the event id is {0},", @event.GetType().FullName, this.GetType().FullName));
            }
            //event id is the state's next version number
            this.State.Version = @event.Version;
        }


    }
}
