using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service
{
    public class TopicService : UpdateEntityService<ITopicRepository, TopicData>, ITopicService
    {
        public IServiceQueryResult<TopicBriefInfoData> GetTopicInfo(object topicId)
        {
            TopicBriefInfoData result = Repository.GetTopicInfo(topicId);
            return ServiceResultFactory.BuildServiceQueryResult<TopicBriefInfoData>(result);
        }

        public IServiceQueryResultList<TopicInfoData> Search(TopicCriteria criteria)
        {
            IEnumerable<TopicInfoData> result = Repository.Search(criteria);
            return ServiceResultFactory.BuildServiceQueryResult<TopicInfoData>(result);
        }

        public IServiceQueryResultList<TopicData> SearchByUser(object userId)
        {
            IEnumerable<TopicData> result = Repository.SearchByUser(userId);
            return ServiceResultFactory.BuildServiceQueryResult<TopicData>(result);
        }
    }
}
