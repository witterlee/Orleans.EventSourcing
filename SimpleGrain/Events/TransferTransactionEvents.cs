using System;
using Orleans.EventSourcing.SimpleInterface;

namespace Orleans.EventSourcing.SimpleGrain.Events
{
    #region events
    [Serializable]
    public abstract class AbstractTransferTransactionEvent : GrainEvent
    {
        public AbstractTransferTransactionEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
        {
            this.TransferTransactionId = transactionId;
            this.TransferTransactionInfo = transactionInfo;
        }

        public Guid TransferTransactionId { get; set; }
        public TransferTransactionInfo TransferTransactionInfo { get; set; }
    }

    [Serializable]
    public class TransferTransactionStartedEvent : AbstractTransferTransactionEvent
    {
        public TransferTransactionStartedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }

        public void Apply(TransferTransactionState state)
        {
            state.Status = TransactionStatus.Started;
            state.TransferTransactionInfo = this.TransferTransactionInfo;
        }

    }
    [Serializable]
    public class AccountValidatePassedEvent : AbstractTransferTransactionEvent
    {
        public AccountValidatePassedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }

        public void Apply(TransferTransactionState state)
        {
            state.Status = TransactionStatus.AccountValidateCompleted;
            state.AccountValidatedAt = this.UTCTimestamp;
        }
    }
    [Serializable]
    public class TransferOutPreparationConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferOutPreparationConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }

        public void Apply(TransferTransactionState state)
        {
            state.TransferOutPreparationConfirmed = true;
            state.TransferOutPreparationConfirmedAt = this.UTCTimestamp;

            if (state.TransferInPreparationConfirmed)
                state.Status = TransactionStatus.PreparationCompleted;
        }
    }
    [Serializable]
    public class TransferInPreparationConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferInPreparationConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }
        public void Apply(TransferTransactionState state)
        {
            state.TransferInPreparationConfirmed = true;
            state.TransferInPreparationConfirmedAt = this.UTCTimestamp;
            if (state.TransferOutPreparationConfirmed)
                state.Status = TransactionStatus.PreparationCompleted;
        }
    }
    [Serializable]
    public class TransferOutConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferOutConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }
        public void Apply(TransferTransactionState state)
        {
            state.TransferOutConfirmed = true;
            state.TransferOutConfirmedAt = this.UTCTimestamp;
            if (state.TransferInConfirmed)
            {
                state.Status = TransactionStatus.Completed;
            }
        }
    }
    [Serializable]
    public class TransferInConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferInConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }

        public void Apply(TransferTransactionState state)
        {
            state.TransferInConfirmed = true;
            state.TransferInConfirmedAt = this.UTCTimestamp;
            if (state.TransferOutConfirmed)
            {
                state.Status = TransactionStatus.Completed;
            }
        }
    }
    [Serializable]
    public class TransferCanceledEvent : GrainEvent
    {
        public TransferCanceledEvent(Guid transactionId, TransactionFaileReason reason)
        {
            this.TransferTransactionId = transactionId;
            this.TransactionFaileReason = reason;
        }
        public Guid TransferTransactionId { get; set; }
        public TransactionFaileReason TransactionFaileReason { get; set; }
        public void Apply(TransferTransactionState state)
        {
            state.TransferCancelAt = this.UTCTimestamp;
            state.Status = TransactionStatus.Canceled;
        }
    }
    #endregion
}
