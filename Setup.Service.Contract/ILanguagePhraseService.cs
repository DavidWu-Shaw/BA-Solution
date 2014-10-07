using Setup.Data;
using Framework.Service;
using Framework.UoW;

namespace Setup.Service.Contract
{
    public interface ILanguagePhraseService : IUpdateEntityService<LanguagePhraseData>
    {
        IServiceQueryResultList<LanguagePhraseData> SearchByLanguage(object languageId);
    }
}
