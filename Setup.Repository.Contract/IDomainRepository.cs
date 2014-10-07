using Framework.Data;
using Setup.Data;

namespace Setup.Repository.Contract
{
    public interface IDomainRepository : IUpdateEntityRepository<DomainData>
    {
    }
}
