using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class ContactDto : BaseDto
    {
        public object EmployeeId { get; set; }
        public object CustomerId { get; set; }
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
        public int CategoryId { get; set; }

        public List<ContactContactMethodDto> ContactContactMethods { get; set; }
        public List<ScheduleEventDto> ScheduleEvents { get; set; }
        public List<ActivityDto> Activitys { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
