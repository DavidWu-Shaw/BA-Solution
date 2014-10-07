using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class EmployeeDto : BaseDto
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Country { get; set; }
        public string CountryState { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

        public List<ActivityDto> Activitys { get; set; }
    }
}
