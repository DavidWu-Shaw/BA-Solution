using System;
using Setup.Data;
using Setup.Repository.Contract;
using Setup.Service.Contract;
using Framework.Service;
using Framework.UoW;
using Framework.Security;

namespace Setup.Service
{
    public class DomainService : UpdateEntityService<IDomainRepository, DomainData>, IDomainService
    {
    }
}
