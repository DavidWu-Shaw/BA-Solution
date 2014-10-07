using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface IScheduleEventRepository : IUpdateEntityRepository<ScheduleEventData>
    {
        IEnumerable<ScheduleEventData> SearchByCustomer(object customerId);
        IEnumerable<ScheduleEventData> SearchByContact(object id);
        IEnumerable<ScheduleEventData> SearchByEmployee(object employeeId);
    }
}
