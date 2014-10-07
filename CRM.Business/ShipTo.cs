using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class ShipTo : BusinessObject<ShipToData>
    {
        [RequiredField("ShipToContactPhoneRequired", "The ContactPhone must be defined.")]
        [StringLength("ShipToContactPhoneLength", "The ContactPhone must have a length less than {1}", MaxLength = 20)]
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

        [StringLength("ShipToCodeLength", "The Code must have a length less than {1}", MaxLength = 50)]
        public string Code
        {
            get
            {
                return Data.Code;
            }
            set
            {
                Data.Code = value;
            }
        }

        public object CustomerId
        {
            get { return Data.CustomerId; }
            set { Data.CustomerId = value; }
        }

        [StringLength("ShipToContactPersonLength", "The ContactPerson must have a length less than {1}", MaxLength = 100)]
        public string ContactPerson
        {
            get { return Data.ContactPerson; }
            set { Data.ContactPerson = value; }
        }

        [StringLength("ShipToAddressLine1Length", "The AddressLine1 must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("ShipToAddressLine2Length", "The AddressLine2 must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("ShipToCountryLength", "The Country must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ShipToCountryStateLength", "The CountryState must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ShipToCityLength", "The City must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ShipToZipCodeLength", "The ZipCode must have a length less than {1}", MaxLength = 50)]
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
    }
}
