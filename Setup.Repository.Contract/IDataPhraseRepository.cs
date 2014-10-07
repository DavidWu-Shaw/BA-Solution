using Framework.Data;
using Setup.Data;
using System.Collections.Generic;

namespace Setup.Repository.Contract
{
    public interface IDataPhraseRepository : IUpdateEntityRepository<DataPhraseData>
    {
        IEnumerable<DataPhraseData> SearchByLanguage(object languageId);
    }
}
