using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Transaction : BusinessObject<TransactionData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            TransactionItems = new ChildBoCollection<TransactionData, TransactionItem, TransactionItemData>(Service, Data.TransactionItemsData, this);
        }

        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            DateSigned = DateTime.Today;
        }

        [ChildCollection]
        public IBusinessObjectCollection<TransactionItem> TransactionItems
        {
            get;
            private set;
        }

        [RequiredField("TransactionTransactionNumberRequired", "The TransactionNumber must be defined.")]
        [StringLength("TransactionTransactionNumberLength", "The TransactionNumber must have a length less than {1}", MaxLength = 50)]
        public string TransactionNumber
        {
            get
            {
                return Data.TransactionNumber;
            }
            set
            {
                Data.TransactionNumber = value;
            }
        }

        public object CustomerId
        {
            get { return Data.CustomerId; }
            set { Data.CustomerId = value; }
        }

        public object ContactId
        {
            get { return Data.ContactId; }
            set { Data.ContactId = value; }
        }

        public DateTime? DateSigned
        {
            get { return Data.DateSigned; }
            set { Data.DateSigned = value; }
        }

        public DateTime? EffectiveDate
        {
            get { return Data.EffectiveDate; }
            set { Data.EffectiveDate = value; }
        }

        public decimal Amount
        {
            get { return Data.Amount; }
            set { Data.Amount = value; }
        }

        public object CurrencyId
        {
            get { return Data.CurrencyId; }
            set { Data.CurrencyId = value; }
        }

        public string Notes
        {
            get { return Data.Notes; }
            set { Data.Notes = value; }
        }
    }
}
