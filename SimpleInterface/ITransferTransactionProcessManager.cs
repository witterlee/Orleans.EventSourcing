using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using Orleans.Concurrency;

namespace Orleans.EventSourcing.SimpleInterface
{
    public interface ITransferTransactionProcessManager : Orleans.IGrainWithIntegerKey
    {
        Task ProcessTransferTransaction(Guid fromAccountId, Guid toAccountId, decimal amount);
    }
}
