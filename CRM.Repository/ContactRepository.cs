using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class ContactRepository : NHUpdateEntityRepository<ContactData>, IContactRepository
    {
        public IEnumerable<ContactData> SearchByCustomer(object customerId)
        {
            ArgumentValidator.IsNotNull("customerId", customerId);

            IEnumerable<ContactData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ContactData>();
                query.AddExpressionEq<ContactData, object>(o => o.CustomerId, customerId);

                result = query.Future<ContactData>();
            });

            return result;
        }

        public IEnumerable<ContactData> SearchByEmployee(object employeeId)
        {
            ArgumentValidator.IsNotNull("EmployeeId", employeeId);

            IEnumerable<ContactData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ContactData>();
                query.AddExpressionEq<ContactData, object>(o => o.EmployeeId, employeeId);

                result = query.Future<ContactData>();
            });

            return result;
        }
    }
}
