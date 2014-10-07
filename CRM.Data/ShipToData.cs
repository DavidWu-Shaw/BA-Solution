using System;
using System.Collections.Generic;
using Framework.Data;

namespace CRM.Data
{
    public class ShipToData : DataObject
    {
        public ShipToData()
        {
        }

        public virtual string Code { get; set; }
        public virtual object CustomerId { get; set; }
        public virtual string ContactPerson { get; set; }
        public virtual string ContactPhone { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string Country { get; set; }
        public virtual string CountryState { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode { get; set; }
    }
}
