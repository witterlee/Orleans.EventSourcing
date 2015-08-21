using System;
using Orleans.EventSourcing.SimpleInterface;

namespace Orleans.EventSourcing.SimpleGrain
{
    public class TransactionPreparation
    {
        public TransactionPreparation(Guid transactionId, TransactionType transactionType, PreparationType preparationType, decimal amount)
        {
            this.TransactionId = transactionId;
            this.TransactionType = transactionType;
            this.PreparationType = preparationType;
            this.Amount = amount;
        }
        public Guid TransactionId { get; set; }
        public TransactionType TransactionType { get; set; }
        public PreparationType PreparationType { get; set; }
        public decimal Amount { get; set; }
    }

}
