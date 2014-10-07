using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.Business;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.Service.Contract;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    internal class TopicSystem : BusinessComponent
    {
        public TopicSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveTopicsBySearch<TDto>(TopicCriteria criteria, IDataConverter<TopicInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("criteria", criteria);
            ArgumentValidator.IsNotNull("converter", converter);

            ITopicService service = UnitOfWork.GetService<ITopicService>();

            var query = service.Search(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveTopicInfo<TDto>(object instanceId, IDataConverter<TopicBriefInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);

            ITopicService service = UnitOfWork.GetService<ITopicService>();
            var query = service.GetTopicInfo(instanceId);
            if (query.HasResult)
            {
                return query.DataToDto(converter);
            }

            return null;
        }

        internal FacadeUpdateResult<TopicData> SaveNewTopic(TopicInfoDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<TopicData> result = new FacadeUpdateResult<TopicData>();
            ITopicService service = UnitOfWork.GetService<ITopicService>();
            Topic instance = RetrieveOrNew<TopicData, Topic, ITopicService>(result.ValidationResult, dto.TopicId);

            if (result.IsSuccessful)
            {
                instance.Title = dto.Title;
                instance.IssuedById = dto.IssuedById;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<TopicData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<TopicData> UpdateTopicVisits(object topicId)
        {
            ArgumentValidator.IsNotNull("topicId", topicId);

            FacadeUpdateResult<TopicData> result = new FacadeUpdateResult<TopicData>();
            ITopicService service = UnitOfWork.GetService<ITopicService>();
            Topic instance = RetrieveOrNew<TopicData, Topic, ITopicService>(result.ValidationResult, topicId);

            if (result.IsSuccessful)
            {
                instance.NumberOfVisits += 1;
                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<TopicData>());
                result.Merge(saveQuery);
            }

            return result;
        }
    }
}
