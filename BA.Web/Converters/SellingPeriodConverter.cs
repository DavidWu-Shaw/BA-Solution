using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class SellingPeriodConverter : IDataConverter<SellingPeriodData, SellingPeriodDto>
    {
        public IEnumerable<SellingPeriodDto> Convert(IEnumerable<SellingPeriodData> entitys)
        {
            List<SellingPeriodDto> dtoList = new List<SellingPeriodDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public SellingPeriodDto Convert(SellingPeriodData entity)
        {
            SellingPeriodDto dto = new SellingPeriodDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.StartTime = entity.StartTime;
            dto.EndTime = entity.EndTime;

            return dto;
        }
    }
}
