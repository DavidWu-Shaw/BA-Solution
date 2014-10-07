using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using PA.Data;
using PA.Service.Contract;
using PA.Repository.Contract;
using Framework.Security;
using Framework.UoW;

namespace PA.Service
{
    public class ProjectService : UpdateEntityService<IProjectRepository, ProjectData>, IProjectService
    {
        public IServiceQueryResult<TaskData> GetTaskByTaskId(object taskId)
        {
            TaskData result = Repository.GetTaskByTaskId(taskId);
            return ServiceResultFactory.BuildServiceQueryResult<TaskData>(result);
        }

        public IServiceQueryResult<ProjectData> GetProjectByTaskId(object taskId)
        {
            ProjectData result = Repository.GetProjectByTaskId(taskId);
            return ServiceResultFactory.BuildServiceQueryResult<ProjectData>(result);
        }
    }
}
