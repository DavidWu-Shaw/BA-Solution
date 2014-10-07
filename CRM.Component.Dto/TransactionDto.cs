using System;
using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class TransactionDto : BaseDto
    {
        public string TransactionNumber { get; set; }
        public object CustomerId { get; set; }
        public object ContactId { get; set; }
        public DateTime? DateSigned { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public decimal Amount { get; set; }
        public object CurrencyId { get; set; }
        public string Notes { get; set; }

        public List<TransactionItemDto> TransactionItems { get; set; }
    }
}
