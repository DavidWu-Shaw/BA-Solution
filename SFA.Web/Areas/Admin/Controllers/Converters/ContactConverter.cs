using System.Collections.Generic;
using System.Linq;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace SFA.Web.Areas.Admin.Controllers.Converters
{
    public class ContactConverter : IDataConverter<ContactData, ContactDto>
    {
        public IEnumerable<ContactDto> Convert(IEnumerable<ContactData> entitys)
        {
            List<ContactDto> dtoList = new List<ContactDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ContactDto Convert(ContactData entity)
        {
            ContactDto dto = new ContactDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.FullName;
            dto.EmployeeId = entity.EmployeeId;
            dto.CustomerId = entity.CustomerId;
            dto.CategoryId = entity.CategoryId;
            dto.FullName = entity.FullName;
            dto.FamilyName = entity.FamilyName;
            dto.Gender = entity.Gender;
            dto.AddressLine1 = entity.AddressLine1;
            dto.AddressLine2 = entity.AddressLine2;
            dto.Country = entity.Country;
            dto.CountryState = entity.CountryState;
            dto.City = entity.City;
            dto.ZipCode = entity.ZipCode;
            dto.Phone = entity.Phone;
            dto.Fax = entity.Fax;
            dto.Email = entity.Email;

            dto.ContactContactMethods = new ContactContactMethodConverter().Convert(entity.ContactContactMethodsData).ToList();

            return dto;
        }
    }
}
