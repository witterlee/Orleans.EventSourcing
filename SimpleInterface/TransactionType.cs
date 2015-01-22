using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Runtime;

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
