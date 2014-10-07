using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class LeadConverter : IDataConverter<LeadData, LeadDto>
    {
        public IEnumerable<LeadDto> Convert(IEnumerable<LeadData> entitys)
        {
            List<LeadDto> dtoList = new List<LeadDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public LeadDto Convert(LeadData entity)
        {
            LeadDto dto = new LeadDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.FullName;
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

            return dto;
        }
    }
}
