using Setup.Data;
using Setup.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using NHibernate.Criterion;
using System.Collections.Generic;
using Framework.Core;

namespace Setup.Repository
{
    public class DataPhraseRepository : NHUpdateEntityRepository<DataPhraseData>, IDataPhraseRepository
    {
        public IEnumerable<DataPhraseData> SearchByLanguage(object languageId)
        {
            ArgumentValidator.IsNotNull("languageId", languageId);

            IEnumerable<DataPhraseData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<DataPhraseData>();
                query.AddExpressionEq<DataPhraseData, object>(o => o.LanguageId, languageId);

                result = query.Future<DataPhraseData>();
            });

            return result;
        }
    }
}
