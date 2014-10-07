using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class TransactionRepository : NHUpdateEntityRepository<TransactionData>, ITransactionRepository
    {
        public IEnumerable<TransactionData> SearchByCustomer(object customerId)
        {
            ArgumentValidator.IsNotNull("customerId", customerId);

            IEnumerable<TransactionData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<TransactionData>();
                query.AddExpressionEq<TransactionData, object>(o => o.CustomerId, customerId);

                result = query.List<TransactionData>();
            });

            return result;
        }

        public IEnumerable<TransactionData> SearchByContact(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<TransactionData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<TransactionData>();
                query.AddExpressionEq<TransactionData, object>(o => o.ContactId, id);

                result = query.List<TransactionData>();
            });

            return result;
        }
    }
}
