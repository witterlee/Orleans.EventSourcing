using System;
using System.Threading.Tasks;

namespace Orleans.EventSourcing.SimpleInterface
{

    public interface ITransferTransactionProcessManager : IGrainWithIntegerKey
    {
        Task ProcessTransferTransaction(Guid fromAccountId, Guid toAccountId, decimal amount);
    }
}
