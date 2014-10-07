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
    public class LanguagePhraseRepository : NHUpdateEntityRepository<LanguagePhraseData>, ILanguagePhraseRepository
    {
        public IEnumerable<LanguagePhraseData> SearchByLanguage(object languageId)
        {
            ArgumentValidator.IsNotNull("languageId", languageId);

            IEnumerable<LanguagePhraseData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<LanguagePhraseData>();
                query.AddExpressionEq<LanguagePhraseData, object>(o => o.LanguageId, languageId);

                result = query.Future<LanguagePhraseData>();
            });

            return result;
        }
    }
}
