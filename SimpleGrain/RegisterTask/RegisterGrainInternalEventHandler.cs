using Orleans.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.EventSourcing;

namespace Orleans.EventSourcing.SimpleGrain.RegisterTask
{ 
    public class RegisterGrainInternalEventHandler : IBootstrapProvider
    {
        public Task Init(string name, IProviderRuntime providerRuntime, IProviderConfiguration config)
        {
            //process uncommit event message(store uncommit event message to event-store)
            //var eventDispatcher = GrainFactory.GetGrain<IEventStoreDispatcher>(0);
            GrainInternalEventHandlerProvider.RegisterInternalEventHandler(this.GetType().Assembly);
             
            return TaskDone.Done;
        }

        public string Name
        {
            get { return "EventStoreInitBootstrapProvider"; }
        }
    }
}
