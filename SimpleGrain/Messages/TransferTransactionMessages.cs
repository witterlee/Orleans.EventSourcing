using System;

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
