using System;

namespace Orleans.EventSourcing.SimpleInterface
{
    public class TransferTransactionInfo
    {
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
        public decimal Amount { get; set; }
    }
}
