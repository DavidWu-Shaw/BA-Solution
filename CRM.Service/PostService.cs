using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service
{
    public class PostService : UpdateEntityService<IPostRepository, PostData>, IPostService
    {
        public IServiceQueryResultList<PostData> SearchByTopic(object topicId)
        {
            IEnumerable<PostData> result = Repository.SearchByTopic(topicId);
            return ServiceResultFactory.BuildServiceQueryResult<PostData>(result);
        }

        public IServiceQueryResultList<PostInfoData> Search(PostCriteria criteria)
        {
            IEnumerable<PostInfoData> result = Repository.Search(criteria);
            return ServiceResultFactory.BuildServiceQueryResult<PostInfoData>(result);
        }
    }
}
