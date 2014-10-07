using Setup.Data;
using Framework.Service;
using Framework.UoW;

namespace Setup.Service.Contract
{
    public interface IDataPhraseService : IUpdateEntityService<DataPhraseData>
    {
        IServiceQueryResultList<DataPhraseData> SearchByLanguage(object languageId);
    }
}
