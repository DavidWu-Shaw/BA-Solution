using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using PA.Data;
using Framework.UoW;

namespace PA.Service.Contract
{
    public interface IUserService : IUpdateEntityService<UserData>
    {
        IServiceQueryResult<UserData> RetrieveByCredential(string username, string passsword);

        bool IsUnique(string p, object p_2);
    }
}
