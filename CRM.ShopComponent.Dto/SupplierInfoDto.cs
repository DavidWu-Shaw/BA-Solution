using System;
using System.Collections.Generic;

namespace CRM.ShopComponent.Dto
{
    public class SupplierInfoDto
    {
        # region Properties name

        public const string FLD_SupplierId = "SupplierId";
        public const string FLD_Name = "Name";
        public const string FLD_Address = "Address";

        # endregion Properties name

        public object SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string CountryState { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
        public string ContactFax { get; set; }
        public object CategoryId { get; set; }
        public int NationId { get; set; }
        public string CategoryText { get; set; }
        public string NationText { get; set; }
        public bool AllowTakeOut { get; set; }
        public bool AllowReserve { get; set; }
        public bool AllowBringWine { get; set; }
        public DateTime? DayStartTime { get; set; }
        public DateTime? DayEndTime { get; set; }
        public decimal Grade { get; set; }
        public byte[] Logo { get; set; }
        public string Website { get; set; }
        public decimal Rating { get; set; }
        public int NumberOfRatings { get; set; }
        public object ProductCatalogId { get; set; }

        public List<ReviewInfoDto> Reviews { get; set; }
        public List<ProductInfoDto> Products { get; set; }
    }
}
