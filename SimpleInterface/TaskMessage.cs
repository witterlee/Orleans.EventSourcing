using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Concurrency;

namespace Orleans.EventSourcing
{ 
    [Immutable]
    [Serializable]
    public abstract class TaskMessage
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }
    }

    [Immutable]
    [Serializable]
    public class SuccessMessage : TaskMessage
    {
        protected SuccessMessage() { Success = true; Message = string.Empty; }
        public static SuccessMessage Instance
        {
            get
            {
                return new SuccessMessage();
            }
        }
    }

    [Immutable]
    [Serializable]
    public class ErrorMessage : TaskMessage
    {
        protected ErrorMessage() { Success = false; Message = string.Empty; }
        public ErrorMessage(string message) { Success = false; Message = message; }
        public static ErrorMessage Instance
        {
            get
            {
                return new ErrorMessage();
            }
        }
    }
}
