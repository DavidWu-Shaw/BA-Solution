using System.Collections.Generic;
using Framework.Data;
using CRM.Data;
using CRM.Data.Criterias;

namespace CRM.Repository.Contract
{
    public interface ITopicRepository : IUpdateEntityRepository<TopicData>
    {
        TopicBriefInfoData GetTopicInfo(object topicId);
        IEnumerable<TopicInfoData> Search(TopicCriteria criteria);
        IEnumerable<TopicData> SearchByUser(object userId);
    }
}
