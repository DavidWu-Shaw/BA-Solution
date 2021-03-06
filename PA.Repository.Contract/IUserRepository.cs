﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Data;
using PA.Data;

namespace PA.Repository.Contract
{
    public interface IUserRepository : IUpdateEntityRepository<UserData>
    {
        UserData RetrieveByCredential(string username, string password);
    }
}
