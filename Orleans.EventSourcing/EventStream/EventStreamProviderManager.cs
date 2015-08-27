using System;

namespace Orleans.EventSourcing
{
    public class EventStreamProviderManager
    {
        private static IEventStreamProviderFactory _eventStreamProviderFactory;

        public static void Initailize(IEventStreamProviderFactory eventStreamProviderFactory)
        {
            if (eventStreamProviderFactory == null)
                throw new ArgumentNullException("eventStreamProviderFactory", "eventStreamProviderFactory is null");

            _eventStreamProviderFactory = eventStreamProviderFactory;
        }

        public static IEventStreamProvider GetEventStreamProvider()
        {
            if (_eventStreamProviderFactory == null)
                throw new Exception("eventStreamProviderFactory not initialized");

            return _eventStreamProviderFactory.CreateEventStreamProvider();
        }
    }
}
