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
            var section = (EventStoreSection)ConfigurationManager.GetSection("eventStoreProvider"); 

            Assert.IsNotNull(section);
            Assert.IsNotNull(section.Providers[0]); 
            Assert.IsTrue(section.Providers[0].ConfigSection == "couchbaseEventStore");
        }
    }
}
