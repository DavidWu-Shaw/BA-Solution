using System.Collections.Generic;
using CRM.Core;
using BA.Web.Common;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class SupplierInfoConverter : IDataConverter<SupplierInfoData, SupplierInfoDto>
    {
        public object LanguageId { get; set; }

        public SupplierInfoConverter()
        {
        }

        public SupplierInfoConverter(object languageId)
        {
            LanguageId = languageId;
        }

        public IEnumerable<SupplierInfoDto> Convert(IEnumerable<SupplierInfoData> entitys)
        {
            List<SupplierInfoDto> dtoList = new List<SupplierInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public SupplierInfoDto Convert(SupplierInfoData entity)
        {
            SupplierInfoDto dto = new SupplierInfoDto();
            dto.SupplierId = entity.SupplierId;
            dto.Name = entity.Name;
            dto.Address = entity.Address;
            dto.Country = entity.Country;
            dto.CountryState = entity.CountryState;
            dto.City = entity.City;
            dto.ZipCode = entity.ZipCode;
            dto.ContactPerson = entity.ContactPerson;
            dto.ContactPhone = entity.ContactPhone;
            dto.ContactEmail = entity.ContactEmail;
            dto.ContactFax = entity.ContactFax;
            dto.CategoryId = entity.CategoryId;
            dto.CategoryText = entity.CategoryText;
            dto.NationId = entity.NationId;
            dto.NationText = entity.NationText;
            dto.AllowTakeOut = entity.AllowTakeOut;
            dto.AllowReserve = entity.AllowReserve;
            dto.AllowBringWine = entity.AllowBringWine;
            dto.DayStartTime = entity.DayStartTime;
            dto.DayEndTime = entity.DayEndTime;
            dto.Grade = entity.Grade;
            dto.Logo = entity.Logo;
            dto.Website = entity.Website;
            dto.Rating = entity.Rating;
            dto.NumberOfRatings = entity.NumberOfRatings;
            dto.ProductCatalogId = entity.ProductCatalogId;

            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && LanguageId != null)
            {
                dto.Name = WebContext.GetLocalizedData(CommonConst.SupplierNameKeyFormatString, entity.SupplierId, LanguageId, entity.Name);
                dto.CategoryText = WebContext.GetLocalizedData(CommonConst.CategoryKeyFormatString, entity.CategoryId, LanguageId, entity.CategoryText);
            }

            return dto;
        }
    }
}
