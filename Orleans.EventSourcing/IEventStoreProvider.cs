using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    /// <summary>
    /// The event store provider interface.
    /// </summary>
    public interface IEventStoreProvider
    {
        /// <summary>
        /// initialize the event store provider, such as a singlton connection etc.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task Initialize(ExpandoObject settings);

        /// <summary>
        /// Create event store provider instance 
        /// <remarks>
        /// </remarks>
        /// </summary>
        /// <returns></returns>
        IEventStore Create();
    }
}
