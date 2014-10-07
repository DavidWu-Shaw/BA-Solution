using System;
using System.Collections.Generic;
using Framework.Data;

namespace CRM.Data
{
    public class QuoteData : DataObject
    {
        public QuoteData()
        {
            QuoteLinesData = new List<QuoteLineData>();
        }

        public virtual string ReferenceNumber { get; set; }
        public virtual int QuoteTypeId { get; set; }
        public virtual int StatusId { get; set; }
        public virtual DateTime TimeCreated { get; set; }
        public virtual decimal Amount { get; set; }
        public virtual object CurrencyId { get; set; }
        public virtual object ProductId { get; set; }
        public virtual object ContactId { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Address { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual string Notes { get; set; }

        public virtual IList<QuoteLineData> QuoteLinesData { get; set; }
    }
}
