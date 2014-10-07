using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class OrderItemConverter : IDataConverter<OrderItemData, OrderItemDto>
    {
        public IEnumerable<OrderItemDto> Convert(IEnumerable<OrderItemData> entitys)
        {
            List<OrderItemDto> dtoList = new List<OrderItemDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public OrderItemDto Convert(OrderItemData entity)
        {
            OrderItemDto dto = new OrderItemDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.ItemDescription;
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
