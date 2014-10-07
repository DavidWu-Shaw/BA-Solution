using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface IOpportunityRepository : IUpdateEntityRepository<OpportunityData>
    {
        IEnumerable<OpportunityData> SearchByCustomer(object customerId);
    }
}
