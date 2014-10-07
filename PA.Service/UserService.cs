using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using PA.Data;
using PA.Service.Contract;
using PA.Repository.Contract;
using Framework.Security;
using Framework.UoW;

namespace PA.Service
{
    public class UserService : UpdateEntityService<IUserRepository, UserData>, IUserService
    {
        public IServiceQueryResult<UserData> RetrieveByCredential(string username, string passsword)
        {
            if (!username.TrimHasValue() || !passsword.TrimHasValue())
            {
                return null;
            }

            string encryptedPassword = SecurityHelper.Encrypt(passsword);

            UserData data = Repository.RetrieveByCredential(username, encryptedPassword);
            return ServiceResultFactory.BuildServiceQueryResult(data);
        }

        #region IUserService Members


        public bool IsUnique(string p, object p_2)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
