using CRM.Core;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;
using SFA.Web.Common;
using System.Collections.Generic;

namespace SFA.Web.Models.Converters
{
    public class ProductToCartItemConverter : IDataConverter<ProductInfoData, CartItemDto>
    {
        public object LanguageId { get; set; }

        public ProductToCartItemConverter()
        {
        }

        public ProductToCartItemConverter(object languageId)
        {
            LanguageId = languageId;
        }

        public IEnumerable<CartItemDto> Convert(IEnumerable<ProductInfoData> entitys)
        {
            List<CartItemDto> dtoList = new List<CartItemDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public CartItemDto Convert(ProductInfoData product)
        {
            CartItemDto dto = new CartItemDto();
            dto.ProductId = product.ProductId;
            dto.ProductName = product.Name;
            dto.SupplierId = product.SupplierId;
            dto.SupplierName = product.SupplierName;
            dto.UnitPrice = product.UnitPrice;
            dto.Amount = dto.UnitPrice * dto.QtyOrdered;

            if (WebContext.Current.ApplicationOption.IsMultiLanguageSupported && LanguageId != null)
            {
                dto.ProductName = WebContext.GetLocalizedData(CommonConst.ProductNameKeyFormatString, product.ProductId, LanguageId, product.Name);
                dto.SupplierName = WebContext.GetLocalizedData(CommonConst.SupplierNameKeyFormatString, product.SupplierId, LanguageId, product.SupplierName);
            }

            return dto;
        }
    }
}
