using System.Collections.Generic;
using CRM.Core;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;
using SFA.Web.Common;

namespace SFA.Web.Models.Converters
{
    public class ProductInfoConverter : IDataConverter<ProductInfoData, ProductInfoDto>
    {
        public object LanguageId { get; set; }

        public ProductInfoConverter()
        {
        }

        public ProductInfoConverter(object languageId)
        {
            LanguageId = languageId;
        }

        public IEnumerable<ProductInfoDto> Convert(IEnumerable<ProductInfoData> entitys)
        {
            List<ProductInfoDto> dtoList = new List<ProductInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ProductInfoDto Convert(ProductInfoData entity)
        {
            ProductInfoDto dto = new ProductInfoDto();
            dto.ProductId = entity.ProductId;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.CategoryId = entity.CategoryId;
            dto.CatalogId = entity.CatalogId;
            dto.SupplierId = entity.SupplierId;
            dto.SellingPeriodId = entity.SellingPeriodId;
            dto.SupplierName = entity.SupplierName;
            dto.CategoryText = entity.CategoryText;
            dto.SellingPeriodName = entity.SellingPeriodName;
            dto.UnitPrice = entity.UnitPrice;
            dto.Sketch = entity.Sketch;
            dto.IsDiscontinued = entity.IsDiscontinued;
            dto.DiscountAmount = entity.DiscountAmount;
            dto.Packaging = entity.Packaging;
            dto.UnitOfMeasure = entity.UnitOfMeasure;
            dto.Rating = entity.Rating;
            dto.NumberOfRatings = entity.NumberOfRatings;
            dto.SellingPeriodStartTime = entity.SellingPeriodStartTime;
            dto.SellingPeriodEndTime = entity.SellingPeriodEndTime;

            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && LanguageId != null)
            {
                dto.Name = WebContext.GetLocalizedData(CommonConst.ProductNameKeyFormatString, entity.ProductId, LanguageId, entity.Name);
                dto.Description = WebContext.GetLocalizedData(CommonConst.ProductDescKeyFormatString, entity.ProductId, LanguageId, entity.Description);
                dto.SupplierName = WebContext.GetLocalizedData(CommonConst.SupplierNameKeyFormatString, entity.SupplierId, LanguageId, entity.SupplierName);
                dto.CategoryText = WebContext.GetLocalizedData(CommonConst.CategoryKeyFormatString, entity.CategoryId, LanguageId, entity.CategoryText);
            }

            return dto;
        }
    }
}
