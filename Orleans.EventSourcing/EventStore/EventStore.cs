using System;
using System.Linq;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public class EventStore<TGrain, TState>
        where TGrain : Grain<TState>, IEventSourcingGrain<TState>
        where TState : EventSourcingState
    {
        private static readonly IEventStoreProvider EventStoreProvider = EventStoreProviderManager.GetProvider<TGrain>();
        private long _afterSnapshotsEventCount;
        private TGrain _grain;
        private IEventStore _eventStore;

        private TState State
        {
            get
            {
                return this._grain.GetState();
            }
        }

        private EventStore() { }

        public static async Task<EventStore<TGrain, TState>> Initialize(TGrain grain, long afterSnapshotsEventCount = 100)
        {
            var instance = new EventStore<TGrain, TState>
            {
                _grain = grain,
                _afterSnapshotsEventCount = afterSnapshotsEventCount,
                _eventStore = await EventStoreProvider.Create<TGrain>()
            };

            return instance;
        }
        public async Task WriteEvent(IEvent @event)
        {
            if (@event != null)
            {
                var writeResult = await this._eventStore.AppendAsync(@event);

                //只在写入成功时，更新内存
                if (writeResult == EventWriteResult.Success)
                {
                    HandleEvent(@event);
                }

                else if (writeResult == EventWriteResult.Duplicate)
                {
                    @event = await this._eventStore.ReadOneAsync(this._grain.GetGrainId(), @event.CommandId);
                }

                else
                    throw new Exception("event store write exception");


                if (await EventStreamProviderManager.GetEventStreamProvider().PublishAsync(@event))
                {
                    if (@event.Version % this._afterSnapshotsEventCount == 0)
                        await this.WriteSnapshot();
                }
                else
                {
                    throw new Exception("publish event stream exception");
                }
            }
        }

        private Task WriteSnapshot()
        {
            return this._grain.WriteSnapshot();
        }
        public async Task ReplayEvents()
        {
            var events = await this._eventStore.ReadFromAsync(this._grain.GetGrainId(), this._grain.GetState().Version + 1);

            if (events.Any())
            {
                events = events.OrderBy(et => et.Version);

                foreach (var evnt in events)
                {
                    HandleEvent(evnt);
                }
            }
        }

        private void HandleEvent(IEvent @event)
        {
            VerifyEvent(@event);
            var eventApplyMethod = GrainInternalEventHandlerProvider.GetInternalEventApplyMethod(@event.GetType());

            if (eventApplyMethod == null)
            {
                throw new Exception(string.Format("Could not find event apply method for [{0}] of [{1}]", @event.GetType().FullName, this.GetType().FullName));
            }

            eventApplyMethod.Invoke(@event, this.State);
            this.State.Version = @event.Version;
        }
        private void VerifyEvent(IEvent @event)
        {
            if (@event.Version != this.State.Version + 1)
            {
                throw new Exception(string.Format("invlid event version for [{0}] of [{1}]", @event.GetType().FullName, this.GetType().FullName));
            }
        }

    }
}
