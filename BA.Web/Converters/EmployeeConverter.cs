using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class EmployeeConverter : IDataConverter<EmployeeData, EmployeeDto>
    {
        public IEnumerable<EmployeeDto> Convert(IEnumerable<EmployeeData> entitys)
        {
            List<EmployeeDto> dtoList = new List<EmployeeDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public EmployeeDto Convert(EmployeeData entity)
        {
            EmployeeDto dto = new EmployeeDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.FullName = entity.FullName;
            dto.JobTitle = entity.JobTitle;
            dto.AddressLine1 = entity.AddressLine1;
            dto.AddressLine2 = entity.AddressLine2;
            dto.Country = entity.Country;
            dto.CountryState = entity.CountryState;
            dto.City = entity.City;
            dto.ZipCode = entity.ZipCode;
            dto.Phone = entity.Phone;

            return dto;
        }
    }
}
