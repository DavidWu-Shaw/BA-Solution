using Setup.Data;
using Setup.Repository.Contract;
using Framework.Data.NHibernate;

namespace Setup.Repository
{
    public class MainMenuRepository : NHUpdateEntityRepository<MainMenuData>, IMainMenuRepository
    {
    }
}
