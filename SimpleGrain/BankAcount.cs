using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.EventSourcing;
using Orleans.EventSourcing.SimpleInterface;
using Orleans.EventSourcing.SimpleGrain.Events;
using Orleans.Providers;
using Orleans.Concurrency;

namespace Orleans.EventSourcing.SimpleGrain
{
    [Reentrant]
    //[StorageProvider(ProviderName = "CouchbaseEventStoreProvider")]
    public class BankAcount : EventSourcingGrain<BankAcount, IBankAcountState>, IBankAccount
    {
        #region interface impl

        async Task<TaskMessage> IBankAccount.Initialize(Guid ownerId)
        {
            if (this.State.OwnerId == null || this.State.OwnerId == Guid.Empty)
            {
                await this.ApplyEvent(new BankAccountInitializeEvent(ownerId));
                return SuccessMessage.Instance;
            }
            else
                return new BankAccountInitialized("this bank account has initialized,Dot initialize again");
        }
        public Task<decimal> GetBalance()
        {
            return Task.FromResult(this.State.Balance);
        }
        Task<bool> IBankAccount.Validate()
        {
            if (this.State.OwnerId == null || this.State.OwnerId == Guid.Empty)
                return Task.FromResult(false);
            else
                return Task.FromResult(true);
        }
        async Task<TaskMessage> IBankAccount.AddTransactionPreparation(Guid transactionId, TransactionType transactionType, PreparationType preparationType, decimal amount)
        {
            if (preparationType == PreparationType.DebitPreparation && this.GetAvailableBalance() < amount)
            {
                return new ErrorMessage("balance not enough");
            }

            var transferTransactionPreparationInfo = new TransactionPreparation(transactionId, transactionType, preparationType, amount);

            if (!this.State.TransactionPreparations.ContainsKey(transactionId))
            {
                await this.ApplyEvent(new TransactionPreparationAddedEvent(transferTransactionPreparationInfo));
            }

            return SuccessMessage.Instance;
        }

        async Task IBankAccount.CommitTransactionPreparation(Guid transactionId)
        {
            var transferTransactionPreparationInfo = default(TransactionPreparation);
            var currentBalance = this.State.Balance;

            if (this.State.TransactionPreparations != null && this.State.TransactionPreparations.TryGetValue(transactionId, out transferTransactionPreparationInfo))
            {
                if (transferTransactionPreparationInfo.PreparationType == PreparationType.DebitPreparation)
                    currentBalance -= transferTransactionPreparationInfo.Amount;
                else
                    currentBalance += transferTransactionPreparationInfo.Amount;

                await this.ApplyEvent(new TransactionPreparationCommittedEvent(transferTransactionPreparationInfo, currentBalance));
            }
        }

        async Task IBankAccount.CancelTransactionPreparation(Guid transactionId)
        {
            var transferTransactionPreparationInfo = default(TransactionPreparation);

            if (this.State.TransactionPreparations == null && this.State.TransactionPreparations.TryGetValue(transactionId, out transferTransactionPreparationInfo))
            {
                await this.ApplyEvent(new TransactionPreparationCanceledEvent(transferTransactionPreparationInfo));
            }
        }

        #endregion
        #region event handlers
        private void Handle(BankAccountInitializeEvent @event)
        {
            this.State.OwnerId = @event.OwnerId;
            this.State.Balance = 100000000;//for test
            this.State.TransactionPreparations = new Dictionary<Guid, TransactionPreparation>();
        }

        private void Handle(TransactionPreparationAddedEvent @event)
        {
            this.State.TransactionPreparations.Add(@event.TransferTransactionPreparation.TransactionId, @event.TransferTransactionPreparation);
        }
        private void Handle(TransactionPreparationCommittedEvent @event)
        {
            this.State.TransactionPreparations.Remove(@event.TransactionPreparation.TransactionId);
            this.State.Balance = @event.CurrentBalance;
        }

        #endregion

        private decimal GetAvailableBalance()
        {
            if (this.State.TransactionPreparations == null || this.State.TransactionPreparations.Count == 0)
            {
                return this.State.Balance;
            }

            var totalDebitTransactionPreparationAmount = 0M;
            foreach (var debitTransactionPreparation in this.State.TransactionPreparations.Values.Where(x => x.PreparationType == PreparationType.DebitPreparation))
            {
                totalDebitTransactionPreparationAmount += debitTransactionPreparation.Amount;
            }

            return this.State.Balance - totalDebitTransactionPreparationAmount;
        }
    }

    public interface IBankAcountState : IEventSourcingState
    {
        Dictionary<Guid, TransactionPreparation> TransactionPreparations { get; set; }
        Guid OwnerId { get; set; }
        decimal Balance { get; set; }
    }
}
