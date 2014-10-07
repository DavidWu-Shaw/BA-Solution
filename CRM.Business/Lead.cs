using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Lead : BusinessObject<LeadData>
    {
        [RequiredField("LeadFullNameRequired", "The FullName must be defined.")]
        [StringLength("LeadFullNameLength", "The FullName must have a length less than {1}", MaxLength = 100)]
        public string FullName
        {
            get
            {
                return Data.FullName;
            }
            set
            {
                Data.FullName = value;
            }
        }

        [StringLength("LeadFamilyNameLength", "The FamilyName must have a length less than {1}", MaxLength = 100)]
        public string FamilyName
        {
            get
            {
                return Data.FamilyName;
            }
            set
            {
                Data.FamilyName = value;
            }
        }

        [StringLength("LeadGenderLength", "The Gender must have a length less than {1}", MaxLength = 1)]
        public string Gender
        {
            get
            {
                return Data.Gender;
            }
            set
            {
                Data.Gender = value;
            }
        }

        [StringLength("LeadAddressLine1Length", "The AddressLine1 must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("LeadAddressLine2Length", "The AddressLine2 must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("LeadCountryLength", "The Country must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("LeadCountryStateLength", "The CountryState must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("LeadCityLength", "The City must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("LeadZipCodeLength", "The ZipCode must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("LeadPhoneLength", "The Phone must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("LeadFaxLength", "The Fax must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("LeadEmailLength", "The Email must have a length less than {1}", MaxLength = 50)]
        public string Email
        {
            get
            {
                return Data.Email;
            }
            set
            {
                Data.Email = value;
            }
        }
    }
}
