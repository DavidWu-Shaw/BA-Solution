using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface IContactRepository : IUpdateEntityRepository<ContactData>
    {
        IEnumerable<ContactData> SearchByCustomer(object customerId);
        IEnumerable<ContactData> SearchByEmployee(object employeeId);
    }
}
