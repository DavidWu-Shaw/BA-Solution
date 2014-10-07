using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class SupplierConverter : IDataConverter<SupplierData, SupplierDto>
    {
        public IEnumerable<SupplierDto> Convert(IEnumerable<SupplierData> entitys)
        {
            List<SupplierDto> dtoList = new List<SupplierDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public SupplierDto Convert(SupplierData entity)
        {
            SupplierDto dto = new SupplierDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.AddressLine1 = entity.AddressLine1;
            dto.AddressLine2 = entity.AddressLine2;
            dto.Country = entity.Country;
            dto.CountryState = entity.CountryState;
            dto.City = entity.City;
            dto.ZipCode = entity.ZipCode;
            dto.ContactPerson = entity.ContactPerson;
            dto.ContactPhone = entity.ContactPhone;
            dto.ContactEmail = entity.ContactEmail;
            dto.ContactFax = entity.ContactFax;
            dto.CategoryId = entity.CategoryId;
            dto.NationId = entity.NationId;
            dto.AllowTakeOut = entity.AllowTakeOut;
            dto.AllowReserve = entity.AllowReserve;
            dto.AllowBringWine = entity.AllowBringWine;
            dto.DayStartTime = entity.DayStartTime;
            dto.DayEndTime = entity.DayEndTime;
            dto.Grade = entity.Grade;
            dto.Logo = entity.Logo;
            dto.Website = entity.Website;
            dto.ProductCatalogId = entity.ProductCatalogId;

            return dto;
        }
    }
}
