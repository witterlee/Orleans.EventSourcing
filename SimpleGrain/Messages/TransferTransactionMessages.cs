using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.EventSourcing;
using Orleans.EventSourcing.SimpleInterface;

namespace Orleans.EventSourcing.SimpleGrain
{
    [Serializable]
    public class BankAccountInitialized : ErrorMessage
    {
        public BankAccountInitialized(string message)
        {
            this.Message = message;
        }
    }

    [Serializable]
    public class BalanceNotEnough : ErrorMessage
    {
        public BalanceNotEnough(string message)
        {
            this.Message = message;
        }
    } 
}
