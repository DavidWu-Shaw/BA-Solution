using Setup.Data;
using Setup.Repository.Contract;
using Setup.Service.Contract;
using Framework.Service;

namespace Setup.Service
{
    public class SetupMenuService : UpdateEntityService<ISetupMenuRepository, SetupMenuData>, ISetupMenuService
    {
    }
}
