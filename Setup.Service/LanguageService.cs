using System;
using Setup.Data;
using Setup.Repository.Contract;
using Setup.Service.Contract;
using Framework.Service;
using Framework.UoW;
using Framework.Security;
using System.Collections.Generic;

namespace Setup.Service
{
    public class LanguageService : UpdateEntityService<ILanguageRepository, LanguageData>, ILanguageService
    {
    }
}
