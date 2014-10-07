using System;
using Setup.Data;
using Setup.Repository.Contract;
using Setup.Service.Contract;
using Framework.Service;
using Framework.UoW;
using Framework.Security;

namespace Setup.Service
{
    public class UserService : UpdateEntityService<IUserRepository, UserData>, IUserService
    {
        public IServiceQueryResult<UserData> RetrieveByCredential(string email, string password)
        {
            if (!email.TrimHasValue() || !password.TrimHasValue())
            {
                return ServiceResultFactory.BuildServiceQueryResult((UserData)null);
            }

            UserData user = Repository.RetrieveByCredential(email, SecurityHelper.Encrypt(password));

            return ServiceResultFactory.BuildServiceQueryResult(user);
        }

        public bool IsExists(string email)
        {
            return Repository.IsExists(email);
        }
    }
}
