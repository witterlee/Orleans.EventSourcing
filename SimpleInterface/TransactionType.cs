namespace Orleans.EventSourcing.SimpleInterface
{
    public enum TransactionType
    {
        /// <summary>存款
        /// </summary>
        DepositTransaction,
        /// <summary>取款
        /// </summary>
        WithdrawTransaction,
        /// <summary>转账
        /// </summary>
        TransferTransaction
    }
}
