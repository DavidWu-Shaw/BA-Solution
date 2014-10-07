using CRM.Data;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface ITopicService : IUpdateEntityService<TopicData>
    {
        IServiceQueryResult<TopicBriefInfoData> GetTopicInfo(object topicId);
        IServiceQueryResultList<TopicInfoData> Search(TopicCriteria criteria);
        IServiceQueryResultList<TopicData> SearchByUser(object userId);
    }
}
