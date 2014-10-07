using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PA.Data;
using PA.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;

namespace PA.Repository
{
    public class ProjectRepository : NHUpdateEntityRepository<ProjectData>, IProjectRepository
    {
        public TaskData GetTaskByTaskId(object taskId)
        {
            TaskData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetTaskByTaskId");
                query.SetInt32("taskId", (int)taskId);
                result = query.UniqueResult<TaskData>();
            });

            return result;
        }

        public ProjectData GetProjectByTaskId(object taskId)
        {
            ProjectData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetProjectByTaskId");
                query.SetInt32("taskId", (int)taskId);
                result = query.UniqueResult<ProjectData>();
            });

            return result;
        }
    }
}
