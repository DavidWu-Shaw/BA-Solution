using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface IActivityService : IUpdateEntityService<ActivityData>
    {
        IServiceQueryResultList<ActivityData> SearchByEmployee(object id);
        IServiceQueryResultList<ActivityData> SearchByCustomer(object customerId);
        IServiceQueryResultList<ActivityData> SearchByContact(object id);
    }
}
