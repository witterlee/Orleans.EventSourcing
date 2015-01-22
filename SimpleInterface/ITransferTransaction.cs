using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Runtime;

namespace Orleans.EventSourcing.SimpleInterface
{
    public interface ITransferTransaction : Orleans.IGrainWithGuidKey
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
