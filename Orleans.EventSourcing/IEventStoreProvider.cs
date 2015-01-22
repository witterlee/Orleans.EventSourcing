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
        /// Create event store provider instance for specifc grain type
        /// <remarks>
        /// maybe you choose save different type grain's event to different table or collection.
        ///       you need give different event store provider instance for every type grain
        /// mayby you save all type grain's event to a same table(collection etc)
        ///       you only need give a singlion event store provider
        /// </remarks>
        /// </summary>
        /// <typeparam name="T">the grain type</typeparam>
        /// <returns></returns>
        IEventStore Create<T>() where T : IEventSourcingGrain;
    }
}
