using System.Collections.Generic;
using System.Linq;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class OrderConverter : IDataConverter<OrderData, OrderDto>
    {
        public IEnumerable<OrderDto> Convert(IEnumerable<OrderData> entitys)
        {
            List<OrderDto> dtoList = new List<OrderDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public OrderDto Convert(OrderData entity)
        {
            OrderDto dto = new OrderDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.OrderNumber;
            dto.OrderNumber = entity.OrderNumber;
            dto.CustomerId = entity.CustomerId;
            dto.ContactId = entity.ContactId;
            dto.TimeOrdered = entity.TimeOrdered;
            dto.TimeShipped = entity.TimeShipped;
            dto.TimeShipBy = entity.TimeShipBy;
            dto.TimeCancelBy = entity.TimeCancelBy;
            dto.QtyOrderedTotal = entity.QtyOrderedTotal;
            dto.Amount = entity.Amount;
            dto.CurrencyId = entity.CurrencyId;
            dto.SupplierId = entity.SupplierId;
            dto.BillToId = entity.BillToId;
            dto.ShipToId = entity.ShipToId;
            dto.StatusId = entity.StatusId;
            dto.Notes = entity.Notes;

            dto.OrderItems = new OrderItemConverter().Convert(entity.OrderItemsData).ToList();

            return dto;
        }
    }
}
