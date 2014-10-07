using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class QuoteLineConverter : IDataConverter<QuoteLineData, QuoteLineDto>
    {
        public IEnumerable<QuoteLineDto> Convert(IEnumerable<QuoteLineData> entitys)
        {
            List<QuoteLineDto> dtoList = new List<QuoteLineDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public QuoteLineDto Convert(QuoteLineData entity)
        {
            QuoteLineDto dto = new QuoteLineDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.ProductId.ToString();
            dto.Quantity = entity.Quantity;
            dto.ProductId = entity.ProductId;

            return dto;
        }
    }
}
