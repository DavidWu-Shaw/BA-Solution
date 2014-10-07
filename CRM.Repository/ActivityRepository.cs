using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class ActivityRepository : NHUpdateEntityRepository<ActivityData>, IActivityRepository
    {
        public IEnumerable<ActivityData> SearchByEmployee(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<ActivityData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ActivityData>();
                query.AddExpressionEq<ActivityData, object>(o => o.EmployeeId, id);

                result = query.List<ActivityData>();
            });

            return result;
        }

        public IEnumerable<ActivityData> SearchByCustomer(object customerId)
        {
            ArgumentValidator.IsNotNull("customerId", customerId);

            IEnumerable<ActivityData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ActivityData>();
                query.AddExpressionEq<ActivityData, object>(o => o.CustomerId, customerId);

                result = query.List<ActivityData>();
            });

            return result;
        }

        public IEnumerable<ActivityData> SearchByContact(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<ActivityData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ActivityData>();
                query.AddExpressionEq<ActivityData, object>(o => o.ContactId, id);

                result = query.List<ActivityData>();
            });

            return result;
        }
    }
}
