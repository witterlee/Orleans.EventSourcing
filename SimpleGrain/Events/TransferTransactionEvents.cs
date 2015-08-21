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
    }
    [Serializable]
    public class AccountValidatePassedEvent : AbstractTransferTransactionEvent
    {
        public AccountValidatePassedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }
    }
    [Serializable]
    public class TransferOutPreparationConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferOutPreparationConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }
    }
    [Serializable]
    public class TransferInPreparationConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferInPreparationConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }
    }
    [Serializable]
    public class TransferOutConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferOutConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }
    }
    [Serializable]
    public class TransferInConfirmedEvent : AbstractTransferTransactionEvent
    {
        public TransferInConfirmedEvent(Guid transactionId, TransferTransactionInfo transactionInfo)
            : base(transactionId, transactionInfo) { }
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
    }
    #endregion
}
