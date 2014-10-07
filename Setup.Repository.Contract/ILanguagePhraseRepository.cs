using Framework.Data;
using Setup.Data;
using System.Collections.Generic;

namespace Setup.Repository.Contract
{
    public interface ILanguagePhraseRepository : IUpdateEntityRepository<LanguagePhraseData>
    {
        IEnumerable<LanguagePhraseData> SearchByLanguage(object languageId);
    }
}
