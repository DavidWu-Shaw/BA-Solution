using System.Collections.Generic;
using Framework.Core;
using PA.Component.Dto;
using PA.Data;

namespace PA.Component.Converters
{
    public class TaskConverter : IDataConverter<TaskData, TaskDto>
    {
        public IEnumerable<TaskDto> Convert(IEnumerable<TaskData> entitys)
        {
            List<TaskDto> dtoList = new List<TaskDto>();
            entitys.ForAll(e => dtoList.Add(Convert(e)));
            return dtoList;
        }

        public TaskDto Convert(TaskData entity)
        {
            TaskDto dto = new TaskDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.ProjectId = entity.Parent.Id;
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.OwnerId = entity.OwnerId;
            if (entity.PriorityId.HasValue)
            {
                dto.PriorityId = entity.PriorityId.Value;
            }
            if (entity.Duration.HasValue)
            {
                dto.Duration = entity.Duration.Value;
            }

            return dto;
        }
    }
}
