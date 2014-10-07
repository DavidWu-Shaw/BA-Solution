using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface IContactService : IUpdateEntityService<ContactData>
    {
        IServiceQueryResultList<ContactData> SearchByCustomer(object customerId);
        IServiceQueryResultList<ContactData> SearchByEmployee(object employeeId);
    }
}
