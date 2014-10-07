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
    public class LanguagePhraseService : UpdateEntityService<ILanguagePhraseRepository, LanguagePhraseData>, ILanguagePhraseService
    {
        public IServiceQueryResultList<LanguagePhraseData> SearchByLanguage(object languageId)
        {
            IEnumerable<LanguagePhraseData> result = Repository.SearchByLanguage(languageId);
            return ServiceResultFactory.BuildServiceQueryResult<LanguagePhraseData>(result);
        }
    }
}
