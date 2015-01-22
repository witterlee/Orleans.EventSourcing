using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Orleans;
using Orleans.Runtime;

namespace Orleans.EventSourcing.SimpleInterface
{
    public enum PreparationType
    {
        /// <summary>预支出（记入借方，When your bank debits your account, money is taken from it and paid to someone else.）
        /// </summary>
        DebitPreparation,
        /// <summary>预收入（记入贷方，When a sum of money is credited to an account, the bank adds that sum of money to the total in the account.）
        /// </summary>
        CreditPreparation,
    }
}
