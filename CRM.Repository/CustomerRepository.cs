using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;

namespace CRM.Repository
{
    public class CustomerRepository : NHUpdateEntityRepository<CustomerData>, ICustomerRepository
    {
    }
}
