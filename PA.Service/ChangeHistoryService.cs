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
    public class ChangeHistoryService : UpdateEntityService<IChangeHistoryRepository, ChangeHistoryData>, IChangeHistoryService
    {
        public ChangeHistoryData[] GetByUser(int userId)
        {
            List<ChangeHistoryData> objects = new List<ChangeHistoryData>();

            //BusinessObjectServiceCallHandler.Execute(delegate
            //{
            //    ChangeHistoryData[] objectsData = Dac.GetByUser(user.ID);

            //    foreach (ChangeHistoryData data in objectsData)
            //    {
            //        objects.Add(BusinessObjectFactory.CreateBO<ChangeHistory>(this, data));
            //    }
            //});

            return objects.ToArray();
        }

        public ChangeHistoryData[] GetByProject(int projectId)
        {
            List<ChangeHistoryData> objects = new List<ChangeHistoryData>();

            //BusinessObjectServiceCallHandler.Execute(delegate
            //{
            //    ChangeHistoryData[] objectsData = Dac.GetByProject(project.ID);

            //    foreach (ChangeHistoryData data in objectsData)
            //    {
            //        objects.Add(BusinessObjectFactory.CreateBO<ChangeHistory>(this, data));
            //    }
            //});

            return objects.ToArray();
        }

        public ChangeHistoryData[] GetByTask(int taskId)
        {
            List<ChangeHistoryData> objects = new List<ChangeHistoryData>();

            //BusinessObjectServiceCallHandler.Execute(delegate
            //{
            //    ChangeHistoryData[] objectsData = Dac.GetByTask(task.ID);

            //    foreach (ChangeHistoryData data in objectsData)
            //    {
            //        objects.Add(BusinessObjectFactory.CreateBO<ChangeHistory>(this, data));
            //    }
            //});

            return objects.ToArray();
        }
    }
}
