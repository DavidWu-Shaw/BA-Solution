using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;
using CRM.Core;

namespace CRM.Repository
{
    public class ScheduleEventRepository : NHUpdateEntityRepository<ScheduleEventData>, IScheduleEventRepository
    {
        public IEnumerable<ScheduleEventData> SearchByCustomer(object customerId)
        {
            ArgumentValidator.IsNotNull("customerId", customerId);

            IEnumerable<ScheduleEventData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ScheduleEventData>();
                query.AddExpressionEq<ScheduleEventData, int>(o => o.ObjectTypeId, (int)EventObjectTypes.Customer);
                query.AddExpressionEq<ScheduleEventData, object>(o => o.ObjectId, customerId);

                result = query.List<ScheduleEventData>();
            });

            return result;
        }

        public IEnumerable<ScheduleEventData> SearchByContact(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<ScheduleEventData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ScheduleEventData>();
                query.AddExpressionEq<ScheduleEventData, int>(o => o.ObjectTypeId, (int)EventObjectTypes.Contact);
                query.AddExpressionEq<ScheduleEventData, object>(o => o.ObjectId, id);

                result = query.List<ScheduleEventData>();
            });

            return result;
        }

        public IEnumerable<ScheduleEventData> SearchByEmployee(object employeeId)
        {
            ArgumentValidator.IsNotNull("employeeId", employeeId);

            IEnumerable<ScheduleEventData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetScheduleEventsByEmployee");
                    query.SetParameter("EmployeeId", employeeId);
                    result = query.List<ScheduleEventData>();
            });

            return result;
        }
    }
}
