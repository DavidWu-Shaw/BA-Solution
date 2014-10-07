using Setup.Data;
using Setup.Repository.Contract;
using Framework.Data.NHibernate;

namespace Setup.Repository
{
    public class ApplicationSettingRepository : NHUpdateEntityRepository<ApplicationSettingData>, IApplicationSettingRepository
    {
    }
}
