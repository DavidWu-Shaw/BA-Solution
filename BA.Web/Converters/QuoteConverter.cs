using System.Collections.Generic;
using System.Linq;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class QuoteConverter : IDataConverter<QuoteData, QuoteDto>
    {
        public IEnumerable<QuoteDto> Convert(IEnumerable<QuoteData> entitys)
        {
            List<QuoteDto> dtoList = new List<QuoteDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public QuoteDto Convert(QuoteData entity)
        {
            QuoteDto dto = new QuoteDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.ReferenceNumber;
            dto.ReferenceNumber = entity.ReferenceNumber;
            dto.Amount = entity.Amount;
            dto.CurrencyId = entity.CurrencyId;
            dto.ProductId = entity.ProductId;
            dto.ContactPerson = entity.ContactPerson;
            dto.Phone = entity.Phone;
            dto.StatusId = entity.StatusId;
            dto.Notes = entity.Notes;

            dto.QuoteLines = new QuoteLineConverter().Convert(entity.QuoteLinesData).ToList();

            return dto;
        }
    }
}
