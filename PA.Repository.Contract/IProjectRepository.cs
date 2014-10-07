using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Data;
using PA.Data;

namespace PA.Repository.Contract
{
    public interface IProjectRepository : IUpdateEntityRepository<ProjectData>
    {
        TaskData GetTaskByTaskId(object taskId);
        ProjectData GetProjectByTaskId(object taskId);
    }
}
