using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Customer : BusinessObject<CustomerData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

        }

        [RequiredField("CustomerNameRequired", "The name must be defined.")]
        [StringLength("CustomerNameLength", "The name must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("CustomerDescriptionLength", "The name must have a length less than {1}", MaxLength = 200)]
        public string Description
        {
            get
            {
                return Data.Description;
            }
            set
            {
                Data.Description = value;
            }
        }

        [StringLength("CustomerAddressLine1Length", "The name must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("CustomerAddressLine2Length", "The name must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("CustomerCountryLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("CustomerCountryStateLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("CustomerCityLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("CustomerZipCodeLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("CustomerPhoneLength", "The name must have a length less than {1}", MaxLength = 50)]
        public string Phone
        {
            get
            {
                return Data.Phone;
            }
            set
            {
                Data.Phone = value;
            }
        }

        [StringLength("CustomerFaxLength", "The name must have a length less than {1}", MaxLength = 50)]
        public string Fax
        {
            get
            {
                return Data.Fax;
            }
            set
            {
                Data.Fax = value;
            }
        }
    }
}
