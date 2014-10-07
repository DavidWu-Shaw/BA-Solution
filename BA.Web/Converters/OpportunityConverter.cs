using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class OpportunityConverter : IDataConverter<OpportunityData, OpportunityDto>
    {
        public IEnumerable<OpportunityDto> Convert(IEnumerable<OpportunityData> entitys)
        {
            List<OpportunityDto> dtoList = new List<OpportunityDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public OpportunityDto Convert(OpportunityData entity)
        {
            OpportunityDto dto = new OpportunityDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.ContactId = entity.ContactId;
            dto.CustomerId = entity.CustomerId;
            dto.ProductId = entity.ProductId;
            dto.EstimateAmount = entity.EstimateAmount;

            return dto;
        }
    }
}
