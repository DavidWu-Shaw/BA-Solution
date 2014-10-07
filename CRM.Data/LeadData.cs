using Framework.Data;

namespace CRM.Data
{
    public class LeadData : DataObject
    {
        public LeadData()
        {
        }

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
    }
}
