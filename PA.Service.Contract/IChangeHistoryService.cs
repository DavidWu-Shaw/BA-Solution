using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using PA.Data;

namespace PA.Service.Contract
{
    public interface IChangeHistoryService : IUpdateEntityService<ChangeHistoryData>
    {
        ChangeHistoryData[] GetByUser(int userId);
        ChangeHistoryData[] GetByProject(int projectId);
        ChangeHistoryData[] GetByTask(int taskId);
    }
}
