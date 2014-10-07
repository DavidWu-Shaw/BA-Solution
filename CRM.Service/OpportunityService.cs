using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class OpportunityService : UpdateEntityService<IOpportunityRepository, OpportunityData>, IOpportunityService
    {
        public IServiceQueryResultList<OpportunityData> SearchByCustomer(object customerId)
        {
            IEnumerable<OpportunityData> result = Repository.SearchByCustomer(customerId);
            return ServiceResultFactory.BuildServiceQueryResult<OpportunityData>(result);
        }
    }
}
