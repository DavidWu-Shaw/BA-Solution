using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class NewsRepository : NHUpdateEntityRepository<NewsData>, INewsRepository
    {
        public IEnumerable<NewsData> SearchByUser(object userId)
        {
            ArgumentValidator.IsNotNull("userId", userId);

            IEnumerable<NewsData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<NewsData>();
                query.AddExpressionEq<NewsData, object>(o => o.IssuedById, userId);

                result = query.Future<NewsData>();
            });

            return result;
        }
    }
}
