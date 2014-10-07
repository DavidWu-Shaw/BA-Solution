using System.Collections.Generic;
using Framework.Data;
using CRM.Data;
using CRM.Data.Criterias;

namespace CRM.Repository.Contract
{
    public interface IPostRepository : IUpdateEntityRepository<PostData>
    {
        IEnumerable<PostInfoData> Search(PostCriteria criteria);
        IEnumerable<PostData> SearchByUser(object userId);
        IEnumerable<PostData> SearchByTopic(object topicId);
    }
}
