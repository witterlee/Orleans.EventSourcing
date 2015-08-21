using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orleans.Core;
namespace Orleans.EventSourcing
{
    public class EventSourcingState : GrainState
    {
        public long Version { get; set; }
    }
}
