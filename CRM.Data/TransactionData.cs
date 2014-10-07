using System;
using System.Collections.Generic;
using Framework.Data;

namespace CRM.Data
{
    public class TransactionData : DataObject
    {
        public TransactionData()
        {
            TransactionItemsData = new List<TransactionItemData>();
        }

        public virtual string TransactionNumber { get; set; }
        public virtual object CustomerId { get; set; }
        public virtual object ContactId { get; set; }
        public virtual DateTime? DateSigned { get; set; }
        public virtual DateTime? EffectiveDate { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual object CurrencyId { get; set; }
        public virtual string Notes { get; set; }

        public virtual IList<TransactionItemData> TransactionItemsData { get; set; }
    }
}
