using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using System.Threading;
using Newtonsoft.Json;

namespace Orleans.EventSourcing
{
    public class EventStore<TGrain, TState>
        where TGrain : IEventSourcingGrain<TState>
        where TState : class,IEventSourcingState
    {
        private static readonly IEventStoreProvider eventStoreProvider = EventStoreProviderManager.GetProvider<TGrain>();
        private ulong afterSnapshotsEventCount;
        private readonly TGrain grain;
        private IEventStore eventStore;
        private static JsonSerializerSettings jsonsetting = new JsonSerializerSettings()
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore 
        };
        private TState State
        {
            get
            {
                return this.grain.GetState();
            }
        }
        public EventStore(TGrain grain, ulong afterSnapshotsEventCount = 100)
        {
            this.grain = grain;
            this.afterSnapshotsEventCount = afterSnapshotsEventCount;
            eventStore = eventStoreProvider.Create<TGrain>();
        }
        public async Task WriteEvent(IEvent @event)
        {
            if (@event != null)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(@event, jsonsetting);
                await eventStore.Append(@event.GrainId, @event.Version, json);
                HandleEvent(@event);
            }

            if (@event.Version % this.afterSnapshotsEventCount == 0)
                await this.WriteSnapshot();
        }

        private Task WriteSnapshot()
        {
            return this.State.WriteStateAsync();
        }
        public async Task ReplayEvents()
        {
            var unapplyEventsJson = await eventStore.ReadFrom(this.grain.GetGrainId(), this.grain.GetState().Version + 1);

            if (unapplyEventsJson.Count() > 0)
            {
                var events = unapplyEventsJson.Select(ConvertJsonToEvent).OrderBy(et => et.Version);
                foreach (var evnt in events)
                {
                    HandleEvent(evnt);
                }
            }
        }

        private void HandleEvent(IEvent @event)
        {
            VerifyEvent(@event);
            var eventHandler = GrainInternalEventHandlerProvider.GetInternalEventHandler(this.grain.GetType(), @event.GetType());

            if (eventHandler == null)
            {
                throw new Exception(string.Format("Could not find event handler for [{0}] of [{1}]", @event.GetType().FullName, this.GetType().FullName));
            }

            eventHandler.Invoke(this.grain, @event);
            this.grain.GetState().Version = @event.Version;
        }
        private void VerifyEvent(IEvent @event)
        {
            if (@event.Version != this.grain.GetState().Version + 1)
            {
                throw new Exception(string.Format("invlid event version for [{0}] of [{1}]", @event.GetType().FullName, this.GetType().FullName));
            }
        }
        private IEvent ConvertJsonToEvent(string eventJson)
        {
            dynamic @event = JsonConvert.DeserializeObject(eventJson, jsonsetting);
            string eventTypeName = @event.Type;
            Type eventType;

            if (!EventNameTypeMapping.TryGetEventType(eventTypeName, out eventType))
            {
                throw new Exception("unknow event type");
            }

            var convertEvent = JsonConvert.DeserializeObject(eventJson, eventType) as IEvent;

            return convertEvent;
        }
    }
}
