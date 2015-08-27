using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orleans.EventSourcing.SimpleGrain.Events;
using Orleans.EventSourcing.SimpleInterface;
using Orleans.Providers;
using Orleans.Runtime;

namespace Orleans.EventSourcing.SimpleGrain
{
    [StorageProvider(ProviderName = "MongoDBStore")]
    public class BankAccount : EventSourcingGrain<BankAccount, BankAcountState>, IBankAccount
    {
        #region interface impl

        async Task IBankAccount.Initialize(Guid ownerId)
        {
            if (this.State.OwnerId == null || this.State.OwnerId == Guid.Empty)
            {
                await this.RaiseEvent(new BankAccountInitializeEvent(ownerId));
            }

            //else
            //    return new BankAccountInitialized("this bank account has initialized,Dot initialize again");
        }
        public Task<decimal> GetBalance()
        {
            return Task.FromResult(this.State.Balance);
        }
        Task<bool> IBankAccount.Validate()
        {
           
            if (this.State.OwnerId == null || this.State.OwnerId == Guid.Empty)
                return Task.FromResult(false);
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
                await this.RaiseEvent(new TransactionPreparationAddedEvent(transferTransactionPreparationInfo));
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

                await this.RaiseEvent(new TransactionPreparationCommittedEvent(transferTransactionPreparationInfo, currentBalance));
            }
        }

        async Task IBankAccount.CancelTransactionPreparation(Guid transactionId)
        {
            var transferTransactionPreparationInfo = default(TransactionPreparation);

            if (this.State.TransactionPreparations == null && this.State.TransactionPreparations.TryGetValue(transactionId, out transferTransactionPreparationInfo))
            {
                await this.RaiseEvent(new TransactionPreparationCanceledEvent(transferTransactionPreparationInfo));
            }
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

    public class BankAcountState : EventSourcingState
    {
        public Dictionary<Guid, TransactionPreparation> TransactionPreparations { get; set; }
        public Guid OwnerId { get; set; }
        public decimal Balance { get; set; }
    }
}
