using Framework.Core;
using System.Collections.Generic;
using System;

namespace CRM.Component.Dto
{
    public class SupplierDto : BaseDto
    {
        # region Properties name

        public const string FLD_Name = "Name";
        public const string FLD_AddressLine1 = "AddressLine1";

        # endregion Properties name

        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string CountryState { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFax { get; set; }
        public int CategoryId { get; set; }
        public int NationId { get; set; }
        public bool AllowTakeOut { get; set; }
        public bool AllowReserve { get; set; }
        public bool AllowBringWine { get; set; }
        public DateTime? DayStartTime { get; set; }
        public DateTime? DayEndTime { get; set; }
        public decimal Grade { get; set; }
        public byte[] Logo { get; set; }
        public string Website { get; set; }
        public object ProductCatalogId { get; set; }

        public List<ProductDto> Products { get; set; }
        public List<OrderDto> Orders { get; set; }
    }
}
