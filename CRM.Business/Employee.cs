using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Employee : BusinessObject<EmployeeData>
    {
        [RequiredField("EmployeeNameRequired", "The name must be defined.")]
        [StringLength("EmployeeNameLength", "The name must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("EmployeeFullNameLength", "The FullName must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("EmployeeJobTitleLength", "The JobTitle must have a length less than {1}", MaxLength = 100)]
        public string JobTitle
        {
            get
            {
                return Data.JobTitle;
            }
            set
            {
                Data.JobTitle = value;
            }
        }

        [StringLength("EmployeeAddressLine1Length", "The name must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("EmployeeAddressLine2Length", "The name must have a length less than {1}", MaxLength = 100)]
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

        [StringLength("EmployeeCountryLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("EmployeeCountryStateLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("EmployeeCityLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("EmployeeZipCodeLength", "The name must have a length less than {1}", MaxLength = 50)]
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

        [StringLength("EmployeePhoneLength", "The name must have a length less than {1}", MaxLength = 50)]
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
    }
}
