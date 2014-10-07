using System.Collections.Generic;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class OrderItemInfoConverter : IDataConverter<OrderItemInfoData, OrderItemInfoDto>
    {
        public IEnumerable<OrderItemInfoDto> Convert(IEnumerable<OrderItemInfoData> entitys)
        {
            List<OrderItemInfoDto> dtoList = new List<OrderItemInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public OrderItemInfoDto Convert(OrderItemInfoData entity)
        {
            OrderItemInfoDto dto = new OrderItemInfoDto();
            dto.OrderItemId = entity.OrderItemId;
            dto.ItemDescription = entity.ItemDescription;
            dto.ProductId = entity.ProductId;
            dto.ProductName = entity.ProductName;
            dto.UnitPrice = entity.UnitPrice;
            dto.QtyOrdered = entity.QtyOrdered;
            dto.Amount = entity.Amount;

            return dto;
        }
    }
}
