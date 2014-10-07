using System;
using System.Collections.Generic;
using Framework.Core;

namespace PA.Component.Dto
{
    public class ProjectDto : BaseDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public object IssuedById { get; set; }
        public DateTime IssuedDate { get; set; }
        public IEnumerable<TaskDto> ProjectTasks;
        public IEnumerable<ProjectMemberDto> ProjectMembers;
    }
}
