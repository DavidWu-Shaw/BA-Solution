using System;
using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class QuoteDto : BaseDto
    {
        # region Properties name

        public const string FLD_ReferenceNumber = "ReferenceNumber";
        public const string FLD_StatusId = "StatusId";
        public const string FLD_Amount = "Amount";

        # endregion Properties name

        public string ReferenceNumber { get; set; }
        public int QuoteTypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime TimeCreated { get; set; }
        public decimal Amount { get; set; }
        public object CurrencyId { get; set; }
        public object ProductId { get; set; }
        public object ContactId { get; set; }
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }

        public List<QuoteLineDto> QuoteLines { get; set; }
    }
}
