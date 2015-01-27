using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Concurrency;
using System.Collections.Concurrent;
using System.Reflection;
using System.Reflection.Emit;

namespace Orleans.EventSourcing
{
    [Serializable]
    public class ConfigInvalidException : Exception
    {

        public ConfigInvalidException() : base() { }
        public ConfigInvalidException(string message) : base(message) { }
        public ConfigInvalidException(string message, Exception ex) : base(message, ex) { }
    }

}
