using System;

namespace Orleans.EventSourcing
{
    [Serializable]
    public class ConfigInvalidException : Exception
    {

        public ConfigInvalidException()
        { }
        public ConfigInvalidException(string message) : base(message) { }
        public ConfigInvalidException(string message, Exception ex) : base(message, ex) { }
    }

}
