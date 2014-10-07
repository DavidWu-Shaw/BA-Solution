using Framework.Data;
using System;

namespace CRM.Data
{
    public class SupplierData : DataObject
    {
        public virtual string Name { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string Country { get; set; }
        public virtual string CountryState { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string ContactPhone { get; set; }
        public virtual string ContactEmail { get; set; }
        public virtual string ContactFax { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int NationId { get; set; }
        public virtual bool AllowTakeOut { get; set; }
        public virtual bool AllowReserve { get; set; }
        public virtual bool AllowBringWine { get; set; }
        public virtual DateTime? DayStartTime { get; set; }
        public virtual DateTime? DayEndTime { get; set; }
        public virtual decimal Grade { get; set; }
        public virtual byte[] Logo { get; set; }
        public virtual string Website { get; set; }
        public virtual object ProductCatalogId { get; set; }
    }
}
