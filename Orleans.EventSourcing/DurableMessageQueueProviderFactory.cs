using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.EventSourcing
{
    public class DurableMessageQueueProviderFactory
    {
        private static bool _initialized;
        private static IDurableMessageQueueProvider _provider;

        public static void Initialize(IDurableMessageQueueProvider provider)
        {
            if (_initialized)
                throw new Exception("DurableMessageQueueProviderFactory have initialized.");

            _provider = provider;
            _initialized = true;
        }
        public IDurableMessageQueueProducter CreateProducter(Guid grainId)
        {
            return _provider.CreateProducter(grainId);
        }
        public IMessageQueueConsumer CreateConsumer(Guid grainId)
        {
            return _provider.CreateConsumer(grainId);
        }

    }
}
