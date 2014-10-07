using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface INewsRepository : IUpdateEntityRepository<NewsData>
    {
        IEnumerable<NewsData> SearchByUser(object userId);
    }
}
