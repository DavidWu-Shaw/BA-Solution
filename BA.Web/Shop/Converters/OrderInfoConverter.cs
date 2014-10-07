using System.Collections.Generic;
using System.Linq;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class OrderInfoConverter : IDataConverter<OrderInfoData, OrderInfoDto>
    {
        public IEnumerable<OrderInfoDto> Convert(IEnumerable<OrderInfoData> entitys)
        {
            List<OrderInfoDto> dtoList = new List<OrderInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public OrderInfoDto Convert(OrderInfoData entity)
        {
            OrderInfoDto dto = new OrderInfoDto();
            dto.OrderId = entity.OrderId;
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
            dto.ShipToId = entity.ShipToId;
            dto.StatusId = entity.StatusId;
            dto.Notes = entity.Notes;

            dto.SupplierName = entity.SupplierName;
            dto.StatusText = entity.StatusText;

            if (entity.ShipToId == null)
            {
                dto.ShipTo = new ShipToDto();
                dto.ShipTo.ContactPhone = entity.ShipToContactPhone;
                dto.ShipTo.ContactPerson = entity.ShipToContactPerson;
                dto.ShipTo.ZipCode = entity.ShipToZipCode;
                dto.ShipTo.AddressLine1 = entity.ShipToAddress;
            }
            else
            {
                dto.ShipTo = new ShipToConverter().Convert(entity.ShipToData);
            }

            dto.OrderItems = new OrderItemInfoConverter().Convert(entity.OrderItemInfosData).ToList();

            return dto;
        }
    }
}
