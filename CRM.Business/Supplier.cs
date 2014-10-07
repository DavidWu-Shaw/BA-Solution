using Framework.Business;
using CRM.Data;
using Framework.Validation;
using System;

namespace CRM.Business
{
    public class Supplier : BusinessObject<SupplierData>
    {
        [RequiredField("SupplierNameRequired", "The name must be defined.")]
        [StringLength("SupplierNameLength", "The name must have a length less than {1}", MaxLength = 100)]
        public string Name
        {
            get
            {
                return Data.Name;
            }
            set
            {
                Data.Name = value;
            }
        }

        [StringLength("SupplierAddressLine1Length", "The name must have a length less than {1}", MaxLength = 100)]
        public string AddressLine1
        {
            get
            {
                return Data.AddressLine1;
            }
            set
            {
                Data.AddressLine1 = value;
            }
        }

        [StringLength("SupplierAddressLine2Length", "The name must have a length less than {1}", MaxLength = 100)]
        public string AddressLine2
        {
            get
            {
                return Data.AddressLine2;
            }
            set
            {
                Data.AddressLine2 = value;
            }
        }

        [StringLength("SupplierCountryLength", "The name must have a length less than {1}", MaxLength = 50)]
        public string Country
        {
            get
            {
                return Data.Country;
            }
            set
            {
                Data.Country = value;
            }
        }

        [StringLength("SupplierCountryStateLength", "The name must have a length less than {1}", MaxLength = 50)]
        public string CountryState
        {
            get
            {
                return Data.CountryState;
            }
            set
            {
                Data.CountryState = value;
            }
        }

        [StringLength("SupplierCityLength", "The name must have a length less than {1}", MaxLength = 50)]
        public string City
        {
            get
            {
                return Data.City;
            }
            set
            {
                Data.City = value;
            }
        }

        [StringLength("SupplierZipCodeLength", "The name must have a length less than {1}", MaxLength = 50)]
        public string ZipCode
        {
            get
            {
                return Data.ZipCode;
            }
            set
            {
                Data.ZipCode = value;
            }
        }

        [StringLength("SupplierContactPhoneLength", "The ContactPhone must have a length less than {1}", MaxLength = 50)]
        public string ContactPhone
        {
            get
            {
                return Data.ContactPhone;
            }
            set
            {
                Data.ContactPhone = value;
            }
        }

        [StringLength("SupplierContactPersonLength", "The ContactPerson must have a length less than {1}", MaxLength = 100)]
        public string ContactPerson
        {
            get
            {
                return Data.ContactPerson;
            }
            set
            {
                Data.ContactPerson = value;
            }
        }

        [StringLength("SupplierContactEmaillLength", "The ContactEmail must have a length less than {1}", MaxLength = 50)]
        public string ContactEmail
        {
            get
            {
                return Data.ContactEmail;
            }
            set
            {
                Data.ContactEmail = value;
            }
        }
        [StringLength("SupplierContactFaxLength", "The ContactFax must have a length less than {1}", MaxLength = 50)]
        public string ContactFax
        {
            get{ return Data.ContactFax;  }
            set { Data.ContactFax = value;}
        }

        public int CategoryId
        {
            get{ return Data.CategoryId;  }
            set { Data.CategoryId = value;}
        }

        public int NationId
        {
            get{ return Data.NationId;  }
            set { Data.NationId = value;}
        }

        public bool AllowTakeOut
        {
            get{ return Data.AllowTakeOut;  }
            set { Data.AllowTakeOut = value;}
        }

        public bool AllowReserve
        {
            get{ return Data.AllowReserve;  }
            set { Data.AllowReserve = value;}
        }

        public bool AllowBringWine
        {
            get{ return Data.AllowBringWine;  }
            set { Data.AllowBringWine = value;}
        }

        public DateTime? DayStartTime
        {
            get{ return Data.DayStartTime;  }
            set { Data.DayStartTime = value;}
        }

        public DateTime? DayEndTime
        {
            get{ return Data.DayEndTime;  }
            set { Data.DayEndTime = value;}
        }

        public decimal Grade
        {
            get { return Data.Grade; }
            set { Data.Grade = value; }
        }
        public byte[] Logo
        {
            get { return Data.Logo; }
            set { Data.Logo = value; }
        }

        [StringLength("SupplierWebsiteLength", "The Website must have a length less than {1}", MaxLength = 200)]
        public string Website
        {
            get { return Data.Website; }
            set { Data.Website = value; }
        }
        public object ProductCatalogId
        {
            get { return Data.ProductCatalogId; }
            set { Data.ProductCatalogId = value; }
        }
    }
}
