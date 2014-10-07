using System.Collections.Generic;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;

namespace BA.Web.Shop.Converters
{
    public class OrderBriefInfoConverter : IDataConverter<OrderBriefInfoData, OrderBriefInfoDto>
    {
        public IEnumerable<OrderBriefInfoDto> Convert(IEnumerable<OrderBriefInfoData> entitys)
        {
            List<OrderBriefInfoDto> dtoList = new List<OrderBriefInfoDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public OrderBriefInfoDto Convert(OrderBriefInfoData entity)
        {
            OrderBriefInfoDto dto = new OrderBriefInfoDto();
            dto.OrderId = entity.OrderId;
            dto.OrderNumber = entity.OrderNumber;
            dto.Amount = entity.Amount;
            dto.StatusId = entity.StatusId;

            dto.StatusText = entity.StatusText;

            return dto;
        }
    }
}
