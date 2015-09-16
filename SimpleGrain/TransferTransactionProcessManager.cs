using System;
using System.Threading.Tasks;
using Orleans.Concurrency;
using Orleans.EventSourcing.SimpleInterface;
using Orleans.Runtime;

namespace Orleans.EventSourcing.SimpleGrain
{
    [StatelessWorker]
    public class TransferTransactionProcessManager : Grain, ITransferTransactionProcessManager
    {
        private const int TransferTransactionProcessManager_ERROR_CODE = 60000;
        private const int TransferTransactionProcessManager_REMINDER_ERROR_CODE = 60001;
        async Task ITransferTransactionProcessManager.ProcessTransferTransaction(Guid fromAccountId, Guid toAccountId, decimal amount)
        {

            var cmdId = RequestContext.Get("CommandId");

            Console.WriteLine(cmdId + amount.ToString());

            var txid = Guid.NewGuid();

            var tx = GrainFactory.GetGrain<ITransferTransaction>(txid);

            await tx.Initialize(fromAccountId, toAccountId, amount);
            try
            {
                await InnerProcessTransferTransaction(tx);
            }
            catch (Exception ex)
            {
                this.GetLogger("TransferTransactionProcessManager").Warn(TransferTransactionProcessManager_ERROR_CODE, "TransferTransactionProcessManager process " + txid + " error", ex);
            }
        }

        private async Task CheckTransferTransaction(Guid txid)
        {
            var tx = GrainFactory.GetGrain<ITransferTransaction>(txid);
            var status = await tx.GetStatus();
            if (status != default(TransactionStatus) && status != TransactionStatus.Completed)
            {
                try
                {
                    await InnerProcessTransferTransaction(tx);
                }
                catch (Exception ex)
                {
                    this.GetLogger("TransferTransactionProcessManager").Warn(TransferTransactionProcessManager_ERROR_CODE, "TransferTransactionProcessManager process " + txid + " error", ex);
                }
            }
            else
            {
                try
                {
                    var reminder = await this.GetReminder(txid.ToString());
                    await this.UnregisterReminder(reminder);
                }
                catch (Exception ex)
                {
                    this.GetLogger("TransferTransactionProcessManager").Warn(TransferTransactionProcessManager_REMINDER_ERROR_CODE, "UnregisterReminder " + txid + " error", ex);

                }
            }

        }

        private async Task InnerProcessTransferTransaction(ITransferTransaction tx)
        {
            var transferTransactionInfo = await tx.GetTransferTransactionInfo();
            var fromAccount = GrainFactory.GetGrain<IBankAccount>(transferTransactionInfo.FromAccountId);
            var toAccount = GrainFactory.GetGrain<IBankAccount>(transferTransactionInfo.ToAccountId);

            if (await tx.GetStatus() == TransactionStatus.Started)
            {
                var validTask1 = fromAccount.Validate();
                var validTask2 = toAccount.Validate();
                await Task.WhenAll(fromAccount.Validate(), toAccount.Validate());

                if (!validTask1.Result) await tx.Cancel(TransactionFaileReason.FromAccountNotExist);
                else if (!validTask2.Result) await tx.Cancel(TransactionFaileReason.ToAccountNotExist);
                else await tx.ConfirmAccountValidatePassed();
            }

            if (await tx.GetStatus() == TransactionStatus.AccountValidateCompleted)
            {
                var transferOutPreparationTask = fromAccount.AddTransactionPreparation(tx.GetPrimaryKey(), TransactionType.TransferTransaction, PreparationType.DebitPreparation, transferTransactionInfo.Amount);
                var transferInPreparationTask = toAccount.AddTransactionPreparation(tx.GetPrimaryKey(), TransactionType.TransferTransaction, PreparationType.CreditPreparation, transferTransactionInfo.Amount);

                await Task.WhenAll(transferOutPreparationTask, transferInPreparationTask);

                if (transferOutPreparationTask.Result.Success)
                    await tx.ConfirmTransferOutPreparation();
                else
                    await tx.Cancel(TransactionFaileReason.BalanceNotEnough);

                if (transferInPreparationTask.Result.Success)
                    await tx.ConfirmTransferInPreparation();
            }

            if (await tx.GetStatus() == TransactionStatus.PreparationCompleted)
            {
                var transferOutTask = fromAccount.CommitTransactionPreparation(tx.GetPrimaryKey());
                var transferInTask = toAccount.CommitTransactionPreparation(tx.GetPrimaryKey());

                await Task.WhenAll(transferOutTask, transferInTask);
                await Task.WhenAll(tx.ConfirmTransferOut(), tx.ConfirmTransferIn());

                //Console.WriteLine("transaction complete success." + transferTransactionInfo.Amount + "$");

            }
        }
    }
}
