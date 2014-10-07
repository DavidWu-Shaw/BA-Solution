using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface IEmployeeRepository : IUpdateEntityRepository<EmployeeData>
    {
    }
}
