using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class ScheduleEventConverter : IDataConverter<ScheduleEventData, ScheduleEventDto>
    {
        public IEnumerable<ScheduleEventDto> Convert(IEnumerable<ScheduleEventData> entitys)
        {
            List<ScheduleEventDto> dtoList = new List<ScheduleEventDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ScheduleEventDto Convert(ScheduleEventData entity)
        {
            ScheduleEventDto dto = new ScheduleEventDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.ScheduledTime = entity.ScheduledTime;
            dto.ObjectId = entity.ObjectId;
            dto.ObjectTypeId = entity.ObjectTypeId;
            dto.ReccuringTypeId = entity.ReccuringTypeId;
            dto.StartDate = entity.StartDate;
            dto.EndDate = entity.EndDate;

            return dto;
        }
    }
}
