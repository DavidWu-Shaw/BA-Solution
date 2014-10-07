using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface ICustomerRepository : IUpdateEntityRepository<CustomerData>
    {
    }
}
