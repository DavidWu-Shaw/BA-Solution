using Setup.Data;
using Framework.Service;
using Framework.UoW;

namespace Setup.Service.Contract
{
    public interface IDomainService : IUpdateEntityService<DomainData>
    {
    }
}
