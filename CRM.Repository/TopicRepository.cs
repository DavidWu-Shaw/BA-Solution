using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;
using CRM.Data.Criterias;

namespace CRM.Repository
{
    public class TopicRepository : NHUpdateEntityRepository<TopicData>, ITopicRepository
    {
        public TopicBriefInfoData GetTopicInfo(object topicId)
        {
            TopicBriefInfoData result = null;
            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetTopicBriefInfo");
                query.SetParameter("TopicId", topicId);
                result = query.UniqueResult<TopicBriefInfoData>();
            });

            return result;
        }

        public IEnumerable<TopicInfoData> Search(TopicCriteria criteria)
        {
            IEnumerable<TopicInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetTopicInfoList");
                if (criteria.ForumId != null)
                {
                    query.SetParameter("ForumId", criteria.ForumId);
                }
                else
                {
                    query.SetString("ForumId", null);
                }
                if (criteria.UserId != null)
                {
                    query.SetParameter("UserId", criteria.UserId);
                }
                else
                {
                    query.SetString("UserId", null);
                }

                if (string.IsNullOrEmpty(criteria.KeyWord))
                {
                    query.SetString("KeyWord", null);
                }
                else
                {
                    query.SetString("KeyWord", criteria.KeyWord.Trim());
                }

                result = query.List<TopicInfoData>();
            });

            return result;
        }

        public IEnumerable<TopicData> SearchByUser(object userId)
        {
            ArgumentValidator.IsNotNull("userId", userId);

            IEnumerable<TopicData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<TopicData>();
                query.AddExpressionEq<TopicData, object>(o => o.IssuedById, userId);

                result = query.Future<TopicData>();
            });

            return result;
        }
    }
}
