using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PA.Data;
using PA.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate.Criterion;
using NHibernate;

namespace PA.Repository
{
    public class UserRepository : NHUpdateEntityRepository<UserData>, IUserRepository
    {
        public UserData RetrieveByCredential(string username, string password)
        {
            UserData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                result = CurrentSession.CreateCriteria<UserData>()
                    .AddExpressionInsensitiveLike<UserData, string>(o => o.Username, username, MatchMode.Exact)
                    .AddExpressionEq<UserData, string>(o => o.Password, password)
                    .UniqueResult<UserData>();
            });

            return result;
        }
    }
}
