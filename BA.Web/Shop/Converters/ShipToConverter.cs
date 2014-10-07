using System.Collections.Generic;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class ShipToConverter : IDataConverter<ShipToData, ShipToDto>
    {
        public IEnumerable<ShipToDto> Convert(IEnumerable<ShipToData> entitys)
        {
            List<ShipToDto> dtoList = new List<ShipToDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ShipToDto Convert(ShipToData entity)
        {
            ShipToDto dto = new ShipToDto();
            dto.Id = entity.Id;
            dto.ContactPerson = entity.ContactPerson;
            dto.CustomerId = entity.CustomerId;
            dto.ContactPhone = entity.ContactPhone;
            dto.AddressLine1 = entity.AddressLine1;
            dto.AddressLine2 = entity.AddressLine2;
            dto.Country = entity.Country;
            dto.CountryState = entity.CountryState;
            dto.City = entity.City;
            dto.ZipCode = entity.ZipCode;

            return dto;
        }
    }
}
