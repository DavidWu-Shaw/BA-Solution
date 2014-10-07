using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace SFA.Web.Areas.Admin.Controllers.Converters
{
    public class ContactContactMethodConverter : IDataConverter<ContactContactMethodData, ContactContactMethodDto>
    {
        public IEnumerable<ContactContactMethodDto> Convert(IEnumerable<ContactContactMethodData> entitys)
        {
            List<ContactContactMethodDto> dtoList = new List<ContactContactMethodDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ContactContactMethodDto Convert(ContactContactMethodData entity)
        {
            ContactContactMethodDto dto = new ContactContactMethodDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }

            dto.Display = entity.ContactMethodValue;
            dto.ContactMethodTypeId = entity.ContactMethodTypeId;
            dto.ContactMethodValue = entity.ContactMethodValue;

            return dto;
        }
    }
}
