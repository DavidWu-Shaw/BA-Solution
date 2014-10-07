using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class ActivityService : UpdateEntityService<IActivityRepository, ActivityData>, IActivityService
    {
        public IServiceQueryResultList<ActivityData> SearchByEmployee(object id)
        {
            IEnumerable<ActivityData> result = Repository.SearchByEmployee(id);
            return ServiceResultFactory.BuildServiceQueryResult<ActivityData>(result);
        }

        public IServiceQueryResultList<ActivityData> SearchByCustomer(object customerId)
        {
            IEnumerable<ActivityData> result = Repository.SearchByCustomer(customerId);
            return ServiceResultFactory.BuildServiceQueryResult<ActivityData>(result);
        }

        public IServiceQueryResultList<ActivityData> SearchByContact(object contactId)
        {
            IEnumerable<ActivityData> result = Repository.SearchByContact(contactId);
            return ServiceResultFactory.BuildServiceQueryResult<ActivityData>(result);
        }
    }
}
