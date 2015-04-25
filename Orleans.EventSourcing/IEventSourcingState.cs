using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{

    public interface IEventSourcingState : IGrainState
    {
        long Version { get; set; }
    }
}
