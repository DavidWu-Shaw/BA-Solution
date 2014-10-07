using System.Collections.Generic;
using System.Linq;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class TransactionConverter : IDataConverter<TransactionData, TransactionDto>
    {
        public IEnumerable<TransactionDto> Convert(IEnumerable<TransactionData> entitys)
        {
            List<TransactionDto> dtoList = new List<TransactionDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public TransactionDto Convert(TransactionData entity)
        {
            TransactionDto dto = new TransactionDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.TransactionNumber;
            dto.TransactionNumber = entity.TransactionNumber;
            dto.CustomerId = entity.CustomerId;
            dto.ContactId = entity.ContactId;
            dto.DateSigned = entity.DateSigned;
            dto.EffectiveDate = entity.EffectiveDate;
            dto.Amount = entity.Amount;
            dto.CurrencyId = entity.CurrencyId;
            dto.Notes = entity.Notes;

            dto.TransactionItems = new TransactionItemConverter().Convert(entity.TransactionItemsData).ToList();

            return dto;
        }
    }
}
