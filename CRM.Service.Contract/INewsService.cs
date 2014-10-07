using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface INewsService : IUpdateEntityService<NewsData>
    {
        IServiceQueryResultList<NewsData> SearchByUser(object userId);
    }
}
