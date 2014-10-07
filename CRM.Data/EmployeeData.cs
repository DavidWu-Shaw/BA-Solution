using Framework.Data;

namespace CRM.Data
{
    public class EmployeeData : DataObject
    {
        public EmployeeData()
        {
        }

        public virtual string Name { get; set; }
        public virtual string FullName { get; set; }
        public virtual string JobTitle { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string Country { get; set; }
        public virtual string CountryState { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Phone { get; set; }
    }
}
