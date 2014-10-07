using CRM.Data;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface IPostService : IUpdateEntityService<PostData>
    {
        IServiceQueryResultList<PostData> SearchByTopic(object topicId);
        IServiceQueryResultList<PostInfoData> Search(PostCriteria criteria);
    }
}
