using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class CustomerConverter : IDataConverter<CustomerData, CustomerDto>
    {
        public IEnumerable<CustomerDto> Convert(IEnumerable<CustomerData> entitys)
        {
            List<CustomerDto> dtoList = new List<CustomerDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public CustomerDto Convert(CustomerData entity)
        {
            CustomerDto dto = new CustomerDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.AddressLine1 = entity.AddressLine1;
            dto.AddressLine2 = entity.AddressLine2;
            dto.Country = entity.Country;
            dto.CountryState = entity.CountryState;
            dto.City = entity.City;
            dto.ZipCode = entity.ZipCode;
            dto.Phone = entity.Phone;
            dto.Fax = entity.Fax;

            return dto;
        }
    }
}
