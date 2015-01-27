using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans; 

namespace Orleans.EventSourcing.SimpleInterface
{

    public interface ITransferTransactionProcessManager : Orleans.IGrainWithIntegerKey
    {
        Task ProcessTransferTransaction(Guid fromAccountId, Guid toAccountId, decimal amount);
    }
}
