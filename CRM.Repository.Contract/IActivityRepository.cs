using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface IActivityRepository : IUpdateEntityRepository<ActivityData>
    {
        IEnumerable<ActivityData> SearchByEmployee(object id);
        IEnumerable<ActivityData> SearchByCustomer(object customerId);
        IEnumerable<ActivityData> SearchByContact(object id);
    }
}
