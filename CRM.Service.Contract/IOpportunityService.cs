using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface IOpportunityService : IUpdateEntityService<OpportunityData>
    {
        IServiceQueryResultList<OpportunityData> SearchByCustomer(object customerId);
    }
}
