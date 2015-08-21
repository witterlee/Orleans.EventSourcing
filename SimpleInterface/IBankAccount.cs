using System;
using System.Threading.Tasks;

namespace Orleans.EventSourcing.SimpleInterface
{
    public interface IBankAccount : IGrainWithGuidKey
    {
        Task Initialize(Guid ownerId);
        Task<bool> Validate();
        Task<TaskMessage> AddTransactionPreparation(Guid transactionId, TransactionType transactionType, PreparationType preparationType, decimal amount);
        Task CommitTransactionPreparation(Guid transactionId);
        Task CancelTransactionPreparation(Guid transactionId); 
        Task<decimal> GetBalance();
    }
}
