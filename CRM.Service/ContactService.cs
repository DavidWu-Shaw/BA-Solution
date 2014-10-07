using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class ContactService : UpdateEntityService<IContactRepository, ContactData>, IContactService
    {
        public IServiceQueryResultList<ContactData> SearchByCustomer(object customerId)
        {
            IEnumerable<ContactData> result = Repository.SearchByCustomer(customerId);
            return ServiceResultFactory.BuildServiceQueryResult<ContactData>(result);
        }

        public IServiceQueryResultList<ContactData> SearchByEmployee(object employeeId)
        {
            IEnumerable<ContactData> result = Repository.SearchByEmployee(employeeId);
            return ServiceResultFactory.BuildServiceQueryResult<ContactData>(result);
        }
    }
}
