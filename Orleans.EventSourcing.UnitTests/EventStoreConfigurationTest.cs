using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Linq;

namespace Orleans.EventSourcing.UnitTests
{
    [TestClass]
    public class EventStoreConfigurationTest
    {
        [TestMethod]
        public void Test_EventStore_Config()
        {
            EventStoreProviderConfigManager eventStoreConfigManager = new EventStoreProviderConfigManager();
            var couchbaseProviderConfig = eventStoreConfigManager.ProviderConfigs.SingleOrDefault(s => s.Name == "CouchbaseEventStoreProvider");
            Assert.IsNotNull(eventStoreConfigManager.ProviderConfigs);
            Assert.IsTrue(eventStoreConfigManager.ProviderConfigs.Count()== 2);
            Assert.IsTrue(couchbaseProviderConfig.Default);
            Assert.IsTrue(!string.IsNullOrEmpty(couchbaseProviderConfig.Type));

            dynamic setting = couchbaseProviderConfig.Settings;

            Assert.IsNotNull((object)setting); 
        }
    }
}
