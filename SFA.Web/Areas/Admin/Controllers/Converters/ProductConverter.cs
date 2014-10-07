using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace SFA.Web.Areas.Admin.Controllers.Converters
{
    public class ProductConverter : IDataConverter<ProductData, ProductDto>
    {
        public IEnumerable<ProductDto> Convert(IEnumerable<ProductData> entitys)
        {
            List<ProductDto> dtoList = new List<ProductDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ProductDto Convert(ProductData entity)
        {
            ProductDto dto = new ProductDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.CategoryId = entity.CategoryId;
            dto.SupplierId = entity.SupplierId;
            dto.UnitPrice = entity.UnitPrice;
            dto.Sketch = entity.Sketch;
            dto.SellingPeriodId = entity.SellingPeriodId;
            dto.IsDiscontinued = entity.IsDiscontinued;
            dto.DiscountAmount = entity.DiscountAmount;
            dto.Packaging = entity.Packaging;
            dto.UnitOfMeasure = entity.UnitOfMeasure;

            return dto;
        }
    }
}