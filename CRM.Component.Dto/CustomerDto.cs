using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class CustomerDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string CountryState { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public List<ContactDto> Contacts { get; set; }
        public List<ScheduleEventDto> ScheduleEvents { get; set; }
        public List<ActivityDto> Activitys { get; set; }
        public List<TransactionDto> Transactions { get; set; }
        public List<OrderDto> Orders { get; set; }
        public List<ShipToDto> ShipTos { get; set; }
    }
}
