namespace Orleans.EventSourcing.SimpleInterface
{
    public enum TransactionFaileReason
    {
        FromAccountNotExist,
        ToAccountNotExist,
        BalanceNotEnough
    }
}
