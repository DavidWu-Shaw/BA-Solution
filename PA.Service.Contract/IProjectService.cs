using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using PA.Data;
using Framework.UoW;

namespace PA.Service.Contract
{
    public interface IProjectService : IUpdateEntityService<ProjectData>
    {
        IServiceQueryResult<TaskData> GetTaskByTaskId(object taskId);
        IServiceQueryResult<ProjectData> GetProjectByTaskId(object taskId);
        //Project[] GetProjectsInvolved(int userID);

        //Project[] GetProjectsByCreator(int userID);
    }
}
