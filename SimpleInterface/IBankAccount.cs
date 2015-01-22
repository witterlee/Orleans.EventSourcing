using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Runtime;
using Orleans.EventSourcing;

namespace Orleans.EventSourcing.SimpleInterface
{
    public interface IBankAccount : Orleans.IGrainWithGuidKey
    {
        Task<TaskMessage> Initialize(Guid ownerId);
        Task<bool> Validate();
        Task<TaskMessage> AddTransactionPreparation(Guid transactionId, TransactionType transactionType, PreparationType preparationType, decimal amount);
        Task CommitTransactionPreparation(Guid transactionId);
        Task CancelTransactionPreparation(Guid transactionId); 
        Task<decimal> GetBalance();
    }
}
