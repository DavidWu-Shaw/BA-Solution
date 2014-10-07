using System.Collections.Generic;
using Framework.Component;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class TopicFacade : BusinessComponent
    {
        public TopicFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            TopicSystem = new TopicSystem(unitOfWork);
        }

        private TopicSystem TopicSystem { get; set; }

        public List<TDto> RetrieveAllTopic<TDto>(IDataConverter<TopicInfoData, TDto> converter)
            where TDto : class
        {
            TopicCriteria criteria = new TopicCriteria();

            List<TDto> instances = TopicSystem.RetrieveTopicsBySearch(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveTopicInfo<TDto>(object id, IDataConverter<TopicBriefInfoData, TDto> converter)
            where TDto : class
        {
            // Get Topic basic info
            TDto info = TopicSystem.RetrieveTopicInfo(id, converter);
            return info;
        }

        public IFacadeUpdateResult<TopicData> CreateTopic(TopicInfoDto topic, PostInfoDto post)
        {
            // Save topic
            UnitOfWork.BeginTransaction();
            FacadeUpdateResult<TopicData> result = TopicSystem.SaveNewTopic(topic);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
                TopicData savedTopic = result.Result;
                // set post.TopicId
                post.TopicId = savedTopic.Id;
                // Save Post
                PostSystem postSystem = new PostSystem(UnitOfWork);
                UnitOfWork.BeginTransaction();
                IFacadeUpdateResult<PostData> resultPost = postSystem.SaveNewPost(post);
                if (resultPost.IsSuccessful)
                {
                    UnitOfWork.CommitTransaction();
                }
                else
                {
                    UnitOfWork.RollbackTransaction();
                }
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<TopicData> UpdateTopicVisits(object topicId)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<TopicData> result = TopicSystem.UpdateTopicVisits(topicId);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }
    }
}
