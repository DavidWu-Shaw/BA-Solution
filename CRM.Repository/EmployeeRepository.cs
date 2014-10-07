using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;

namespace CRM.Repository
{
    public class EmployeeRepository : NHUpdateEntityRepository<EmployeeData>, IEmployeeRepository
    {
    }
}
