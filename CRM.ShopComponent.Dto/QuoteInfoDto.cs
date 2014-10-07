using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRM.ShopComponent.Dto
{
    public class QuoteInfoDto
    {
        public object QuoteId { get; set; }
        public string ReferenceNumber { get; set; }
        public int QuoteTypeId { get; set; }
        public int StatusId { get; set; }
        public DateTime TimeCreated { get; set; }
        public decimal Amount { get; set; }
        public object CurrencyId { get; set; }
        public object ProductId { get; set; }
        public object ContactId { get; set; }
        public string ContactPerson { get; set; }
        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Notes { get; set; }

        public string StatusText { get; set; }
        public string ProductName { get; set; }

        public List<QuoteLineInfoDto> QuoteLines { get; set; }
    }
}
