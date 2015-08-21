using System;

namespace Orleans.EventSourcing.SimpleGrain.Events
{
    #region Events

    [Serializable]
    public class BankAccountInitializeEvent : GrainEvent
    {
        public BankAccountInitializeEvent(Guid ownerId)
        {
            this.OwnerId = ownerId;
        }
        public Guid OwnerId { get; private set; }
    }

    [Serializable]
    public class TransactionPreparationAddedEvent : GrainEvent
    {
        public TransactionPreparationAddedEvent(TransactionPreparation transferPreparation)
        {
            this.TransferTransactionPreparation = transferPreparation;
        }
        public TransactionPreparation TransferTransactionPreparation { get; set; }
    }

    [Serializable]
    public class TransactionPreparationCommittedEvent : GrainEvent
    {
        public TransactionPreparationCommittedEvent(TransactionPreparation transactionPreparation, decimal currentBalance)
        {
            this.TransactionPreparation = transactionPreparation;
            this.CurrentBalance = currentBalance;
        }
        public TransactionPreparation TransactionPreparation { get; set; }
        public decimal CurrentBalance { get; set; }
    }

    [Serializable]
    public class TransactionPreparationCanceledEvent : GrainEvent
    {
        public TransactionPreparationCanceledEvent(TransactionPreparation transactionPreparation)
        {
            this.TransactionPreparation = transactionPreparation;
        }
        public TransactionPreparation TransactionPreparation { get; set; }
    }
    #endregion
}
