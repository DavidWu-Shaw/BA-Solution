using Setup.Data;
using Setup.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using NHibernate.Criterion;

namespace Setup.Repository
{
    public class LanguageRepository : NHUpdateEntityRepository<LanguageData>, ILanguageRepository
    {
    }
}
