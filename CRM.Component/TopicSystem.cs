using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class TopicSystem : BusinessComponent
    {
        public TopicSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllTopic<TDto>(IDataConverter<TopicData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ITopicService service = UnitOfWork.GetService<ITopicService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveTopicsByUser<TDto>(object instanceId, IDataConverter<TopicData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            ITopicService service = UnitOfWork.GetService<ITopicService>();

            var query = service.SearchByUser(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewTopic<TDto>(object instanceId, IDataConverter<TopicData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ITopicService service = UnitOfWork.GetService<ITopicService>();
            FacadeUpdateResult<TopicData> result = new FacadeUpdateResult<TopicData>();
            Topic instance = RetrieveOrNew<TopicData, Topic, ITopicService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<TopicData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<TopicData> SaveTopic(TopicDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<TopicData> result = new FacadeUpdateResult<TopicData>();
            ITopicService service = UnitOfWork.GetService<ITopicService>();
            Topic instance = RetrieveOrNew<TopicData, Topic, ITopicService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Title = dto.Title;
                instance.ForumId = dto.ForumId;
                instance.IssuedById = dto.IssuedById;
                instance.IsSticky = dto.IsSticky;
                instance.DenyReply = dto.DenyReply;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<TopicData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<TopicData> DeleteTopic(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<TopicData> result = new FacadeUpdateResult<TopicData>();
            ITopicService service = UnitOfWork.GetService<ITopicService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Topic instance = query.ToBo<Topic>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "TopicCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ITopicService service = UnitOfWork.GetService<ITopicService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (TopicData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Title));
                }
            }

            return dataSource;
        }
    }
}
