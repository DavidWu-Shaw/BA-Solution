using System.Collections.Generic;
using Framework.Data;

namespace CRM.Data
{
    public class ContactData : DataObject
    {
        public ContactData()
        {
            ContactContactMethodsData = new List<ContactContactMethodData>();
        }

        public virtual object EmployeeId { get; set; }
        public virtual object CustomerId { get; set; }
        public virtual string FullName { get; set; }
        public virtual string FamilyName { get; set; }
        public virtual string Gender { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string Country { get; set; }
        public virtual string CountryState { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Email { get; set; }
        public virtual int CategoryId { get; set; }

        public virtual IList<ContactContactMethodData> ContactContactMethodsData { get; set; }
    }
}
