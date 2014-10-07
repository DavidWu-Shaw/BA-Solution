using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class ActivityConverter : IDataConverter<ActivityData, ActivityDto>
    {
        public IEnumerable<ActivityDto> Convert(IEnumerable<ActivityData> entitys)
        {
            List<ActivityDto> dtoList = new List<ActivityDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ActivityDto Convert(ActivityData entity)
        {
            ActivityDto dto = new ActivityDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }

            dto.Display = entity.ActivityName;
            dto.ActivityName = entity.ActivityName;
            dto.EmployeeId = entity.EmployeeId;
            dto.ContactId = entity.ContactId;
            dto.CustomerId = entity.CustomerId;
            dto.ActivityTypeId = entity.ActivityTypeId;
            dto.Notes = entity.Notes;
            dto.StartTime = entity.StartTime;
            dto.EndTime = entity.EndTime;
            dto.TimeSpent = entity.TimeSpent;

            return dto;
        }
    }
}
