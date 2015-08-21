namespace Orleans.EventSourcing.SimpleInterface
{
    public enum TransactionStatus
    {
        Started = 1,
        AccountValidateCompleted,
        PreparationCompleted,
        Completed,
        Canceled
    }
}
