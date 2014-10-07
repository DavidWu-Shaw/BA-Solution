using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class TopicFacade : BusinessComponent
    {
        public TopicFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            TopicSystem = new TopicSystem(unitOfWork);
        }

        private TopicSystem TopicSystem { get; set; }

        public List<TDto> RetrieveAllTopic<TDto>(IDataConverter<TopicData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = TopicSystem.RetrieveAllTopic(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveTopicsByUser<TDto>(object userId, IDataConverter<TopicData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = TopicSystem.RetrieveTopicsByUser(userId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewTopic<TDto>(object instanceId, IDataConverter<TopicData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto instance = TopicSystem.RetrieveOrNewTopic(instanceId, converter);
            UnitOfWork.CommitTransaction();
            return instance;
        }

        public IFacadeUpdateResult<TopicData> SaveTopic(TopicDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<TopicData> result = TopicSystem.SaveTopic(dto);
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

        public IFacadeUpdateResult<TopicData> DeleteTopic(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<TopicData> result = TopicSystem.DeleteTopic(id);
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
