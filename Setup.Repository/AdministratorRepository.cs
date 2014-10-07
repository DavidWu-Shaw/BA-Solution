using Setup.Data;
using Setup.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using NHibernate.Criterion;

namespace Setup.Repository
{
    public class AdministratorRepository : NHUpdateEntityRepository<AdministratorData>, IAdministratorRepository
    {
        public AdministratorData RetrieveByCredential(string username, string password)
        {
            AdministratorData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                result = CurrentSession.CreateCriteria<AdministratorData>()
                    .AddExpressionEq<AdministratorData, string>(o => o.Username, username)
                    .AddExpressionEq<AdministratorData, string>(o => o.Password, password)
                    .UniqueResult<AdministratorData>();
            });

            return result;
        }

        public bool IsExists(string username)
        {
            bool isExists = false;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                isExists = CurrentSession.CreateCriteria<AdministratorData>()
                    .AddExpressionEq<AdministratorData, string>(o => o.Username, username)
                    .SetProjection(Projections.RowCount())
                    .UniqueResult<int>() != 0;
            });

            return isExists;
        }
    }
}
