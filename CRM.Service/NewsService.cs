using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class NewsService : UpdateEntityService<INewsRepository, NewsData>, INewsService
    {
        public IServiceQueryResultList<NewsData> SearchByUser(object userId)
        {
            IEnumerable<NewsData> result = Repository.SearchByUser(userId);
            return ServiceResultFactory.BuildServiceQueryResult<NewsData>(result);
        }
    }
}
