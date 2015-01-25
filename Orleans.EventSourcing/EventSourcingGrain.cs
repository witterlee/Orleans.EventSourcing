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
        private const int AFTER_SNAPSHOT_SEVENT_COUNT = 100;
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

        protected async Task ApplyEvent(GrainEvent @event)
        {
            @event.GrainId = this.GetPrimaryKey();
            @event.Version = this.GetState().Version + 1;
            @event.UTCTimestamp = DateTime.Now.ToUniversalTime();

            await WriteEvent(@event);
            this.HandleEvent(@event);
        }


        public async override Task OnActivateAsync()
        {
            ReplayEvents();
            await base.OnActivateAsync();
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

        public async Task WriteEvent(IEvent @event)
        {
            var eventDispatcher = GrainFactory.GetGrain<IEventStoreDispatcher>(0);
            await eventDispatcher.Append(this.GetPrimaryKey(), @event.Version, @event);

            var grainId = this.GetPrimaryKey();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(@event);

            if (@event.Version % AFTER_SNAPSHOT_SEVENT_COUNT == 0)
                await this.WriteSnapshot();
        }

        private Task WriteSnapshot()
        {
            return this.State.WriteStateAsync();
        }
        private async void ReplayEvents()
        {
            var grainId = this.GetPrimaryKey();
            var currentVersion = this.State.Version;

            var eventDispatcher = GrainFactory.GetGrain<IEventStoreDispatcher>(0);
            var unapplyEvents = await eventDispatcher.ReadFrom(grainId, currentVersion);

            if (unapplyEvents.Count() > 0)
            {
                foreach (var evnt in unapplyEvents)
                {
                    HandleEvent(evnt);
                }
            }
        }
    }
}
