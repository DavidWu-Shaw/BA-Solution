using System.Collections.Generic;
using Framework.Core;
using PA.Component.Dto;
using PA.Data;

namespace PA.Component.Converters
{
    public class ProjectConverter : IDataConverter<ProjectData, ProjectDto>
    {
        public IEnumerable<ProjectDto> Convert(IEnumerable<ProjectData> entitys)
        {
            List<ProjectDto> dtoList = new List<ProjectDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public ProjectDto Convert(ProjectData entity)
        {
            ProjectDto dto = new ProjectDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Code = entity.Code;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedDate = entity.IssuedDate;

            dto.ProjectTasks = new TaskConverter().Convert(entity.ProjectTasksData);

            return dto;
        }
    }
}
