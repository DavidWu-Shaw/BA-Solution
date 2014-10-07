using Framework.Core;

namespace CRM.Component.Dto
{
    public class LeadDto : BaseDto
    {
        public string FullName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string CountryState { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
    }
}
