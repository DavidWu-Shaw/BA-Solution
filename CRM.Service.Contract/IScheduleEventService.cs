using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface IScheduleEventService : IUpdateEntityService<ScheduleEventData>
    {
        IServiceQueryResultList<ScheduleEventData> SearchByCustomer(object customerId);
        IServiceQueryResultList<ScheduleEventData> SearchByContact(object id);
        IServiceQueryResultList<ScheduleEventData> SearchByEmployee(object employeeId);
    }
}
