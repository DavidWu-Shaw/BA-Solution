using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class OpportunityRepository : NHUpdateEntityRepository<OpportunityData>, IOpportunityRepository
    {
        public IEnumerable<OpportunityData> SearchByCustomer(object customerId)
        {
            ArgumentValidator.IsNotNull("customerId", customerId);

            IEnumerable<OpportunityData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<OpportunityData>();
                query.AddExpressionEq<OpportunityData, object>(o => o.CustomerId, customerId);

                result = query.Future<OpportunityData>();
            });

            return result;
        }
    }
}
