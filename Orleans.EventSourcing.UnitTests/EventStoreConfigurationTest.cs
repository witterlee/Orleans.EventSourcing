using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Orleans.EventSourcing.UnitTests
{
    [TestClass]
    public class EventStoreConfigurationTest
    {
        [TestMethod]
        public void Test_EventStore_Config()
        { 
            var section = (EventStoreSection)ConfigurationManager.GetSection("eventStoreProvider"); 

            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Providers[0]); 
            Assert.IsTrue(section.Providers[0].ConfigSection == "couchbaseEventStore");
        }
    }
}
