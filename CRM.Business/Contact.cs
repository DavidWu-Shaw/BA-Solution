using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Contact : BusinessObject<ContactData>
    {
        protected override void OnInitialize()
        {
            base.OnInitialize();

            ContactContactMethods = new ChildBoCollection<ContactData, ContactContactMethod, ContactContactMethodData>(Service, Data.ContactContactMethodsData, this);
        }

        [ChildCollection]
        public IBusinessObjectCollection<ContactContactMethod> ContactContactMethods
        {
            get;
            private set;
        }

        public object EmployeeId
        {
            get { return Data.EmployeeId; }
            set { Data.EmployeeId = value; }
        }

        public object CustomerId
        {
            get { return Data.CustomerId; }
            set { Data.CustomerId = value; }
        }

        public int CategoryId
        {
            get { return Data.CategoryId; }
            set { Data.CategoryId = value; }
        }

        [RequiredField("ContactFullNameRequired", "The FullName must be defined.")]
        [StringLength("ContactFullNameLength", "The FullName must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("ContactFamilyNameLength", "The FamilyName must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("ContactGenderLength", "The Gender must have a length less than {1}", MaxLength = 1)]
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

        [StringLength("ContactAddressLine1Length", "The AddressLine1 must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("ContactAddressLine2Length", "The AddressLine2 must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("ContactCountryLength", "The Country must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ContactCountryStateLength", "The CountryState must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ContactCityLength", "The City must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ContactZipCodeLength", "The ZipCode must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ContactPhoneLength", "The Phone must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ContactFaxLength", "The Fax must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("ContactEmailLength", "The Email must have a length less than {1}", MaxLength = 50)]
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
