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
    public class PostRepository : NHUpdateEntityRepository<PostData>, IPostRepository
    {
        public IEnumerable<PostInfoData> Search(PostCriteria criteria)
        {
            IEnumerable<PostInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetPostInfoList");
                if (criteria.TopicId != null)
                {
                    query.SetParameter("TopicId", criteria.TopicId);
                }
                else
                {
                    query.SetString("TopicId", null);
                }
                if (criteria.UserId != null)
                {
                    query.SetParameter("UserId", criteria.UserId);
                }
                else
                {
                    query.SetString("UserId", null);
                }

                result = query.List<PostInfoData>();
            });

            return result;
        }

        public IEnumerable<PostData> SearchByUser(object userId)
        {
            ArgumentValidator.IsNotNull("userId", userId);

            IEnumerable<PostData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<PostData>();
                query.AddExpressionEq<PostData, object>(o => o.IssuedById, userId);

                result = query.Future<PostData>();
            });

            return result;
        }

        public IEnumerable<PostData> SearchByTopic(object topicId)
        {
            ArgumentValidator.IsNotNull("SupplierId", topicId);

            IEnumerable<PostData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<PostData>();
                query.AddExpressionEq<PostData, object>(o => o.TopicId, topicId);

                result = query.Future<PostData>();
            });

            return result;
        }
    }
}
