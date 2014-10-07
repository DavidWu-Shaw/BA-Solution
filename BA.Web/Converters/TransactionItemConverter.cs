using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class TransactionItemConverter : IDataConverter<TransactionItemData, TransactionItemDto>
    {
        public IEnumerable<TransactionItemDto> Convert(IEnumerable<TransactionItemData> entitys)
        {
            List<TransactionItemDto> dtoList = new List<TransactionItemDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public TransactionItemDto Convert(TransactionItemData entity)
        {
            TransactionItemDto dto = new TransactionItemDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.ItemDescription;
            dto.ItemDescription = entity.ItemDescription;
            dto.ProductId = entity.ProductId;
            dto.Quantity = entity.Quantity;
            dto.UnitPrice = entity.UnitPrice;
            dto.Amount = entity.Amount;

            return dto;
        }
    }
}
