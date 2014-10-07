using Setup.Data;
using Setup.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using NHibernate.Criterion;

namespace Setup.Repository
{
    public class UserRepository : NHUpdateEntityRepository<UserData>, IUserRepository
    {
        public UserData RetrieveByCredential(string email, string password)
        {
            UserData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                result = CurrentSession.CreateCriteria<UserData>()
                    .AddExpressionEq<UserData, string>(o => o.Email, email)
                    .AddExpressionEq<UserData, string>(o => o.Password, password)
                    .UniqueResult<UserData>();
            });

            return result;
        }

        public bool IsExists(string email)
        {
            bool isExists = false;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                isExists = CurrentSession.CreateCriteria<UserData>()
                    .AddExpressionEq<UserData, string>(o => o.Email, email)
                    .SetProjection(Projections.RowCount())
                    .UniqueResult<int>() != 0;
            });

            return isExists;
        }
    }
}
