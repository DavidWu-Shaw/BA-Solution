using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class ScheduleEventService : UpdateEntityService<IScheduleEventRepository, ScheduleEventData>, IScheduleEventService
    {
        public IServiceQueryResultList<ScheduleEventData> SearchByCustomer(object customerId)
        {
            IEnumerable<ScheduleEventData> result = Repository.SearchByCustomer(customerId);
            return ServiceResultFactory.BuildServiceQueryResult<ScheduleEventData>(result);
        }

        public IServiceQueryResultList<ScheduleEventData> SearchByContact(object id)
        {
            IEnumerable<ScheduleEventData> result = Repository.SearchByContact(id);
            return ServiceResultFactory.BuildServiceQueryResult<ScheduleEventData>(result);
        }

        public IServiceQueryResultList<ScheduleEventData> SearchByEmployee(object employeeId)
        {
            IEnumerable<ScheduleEventData> result = Repository.SearchByEmployee(employeeId);
            return ServiceResultFactory.BuildServiceQueryResult<ScheduleEventData>(result);
        }
    }
}
