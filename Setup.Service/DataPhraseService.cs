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
    public class DataPhraseService : UpdateEntityService<IDataPhraseRepository, DataPhraseData>, IDataPhraseService
    {
        public IServiceQueryResultList<DataPhraseData> SearchByLanguage(object languageId)
        {
            IEnumerable<DataPhraseData> result = Repository.SearchByLanguage(languageId);
            return ServiceResultFactory.BuildServiceQueryResult<DataPhraseData>(result);
        }
    }
}
