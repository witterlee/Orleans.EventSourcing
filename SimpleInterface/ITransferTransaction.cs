using System;
using System.Threading.Tasks;

namespace Orleans.EventSourcing.SimpleInterface
{
    public interface ITransferTransaction : IGrainWithGuidKey
    {
        Task Initialize(Guid fromAccountId, Guid toAccountId, decimal amount);
        Task ConfirmAccountValidatePassed();
        Task ConfirmTransferOutPreparation();
        Task ConfirmTransferInPreparation();
        Task ConfirmTransferOut();
        Task ConfirmTransferIn();
        Task Cancel(TransactionFaileReason reason);
        Task<TransactionStatus> GetStatus();
        Task<TransferTransactionInfo> GetTransferTransactionInfo();
    }
}
