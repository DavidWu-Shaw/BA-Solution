using System;
using System.Collections.Generic;
using System.Text;
using Framework.Service;
using SubjectEngine.Data;
using SubjectEngine.Service.Contract;
using SubjectEngine.Repository.Contract;
using Framework.Security;
using Framework.UoW;

namespace SubjectEngine.Service
{
    public class DEntityService : UpdateEntityService<IDEntityRepository, DEntityData>, IDEntityService
    {
       

    }
}
