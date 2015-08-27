using System;
using System.Collections.Generic;

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

        public void Apply(BankAcountState state)
        {
            state.OwnerId = this.OwnerId;
            state.Balance = 100000000;//for test
            state.TransactionPreparations = new Dictionary<Guid, TransactionPreparation>();
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

        public void Apply(BankAcountState state)
        {
            state.TransactionPreparations.Add(this.TransferTransactionPreparation.TransactionId, this.TransferTransactionPreparation);
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

        public void Apply(BankAcountState state)
        {
            state.TransactionPreparations.Remove(this.TransactionPreparation.TransactionId);
            state.Balance = this.CurrentBalance;
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

        public void Apply(BankAcountState state)
        {
            state.TransactionPreparations.Remove(this.TransactionPreparation.TransactionId);
        }

        public TransactionPreparation TransactionPreparation { get; set; }
    }
    #endregion
}
