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
    internal class PostSystem : BusinessComponent
    {
        public PostSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllPost<TDto>(IDataConverter<PostData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IPostService service = UnitOfWork.GetService<IPostService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrievePostsByTopic<TDto>(object topicId, IDataConverter<PostData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("topicId", topicId);
            ArgumentValidator.IsNotNull("converter", converter);

            IPostService service = UnitOfWork.GetService<IPostService>();
            var query = service.SearchByTopic(topicId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return new List<TDto>();
        }

        internal TDto RetrieveOrNewPost<TDto>(object instanceId, IDataConverter<PostData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IPostService service = UnitOfWork.GetService<IPostService>();
            FacadeUpdateResult<PostData> result = new FacadeUpdateResult<PostData>();
            Post instance = RetrieveOrNew<PostData, Post, IPostService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<PostData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<PostData> SavePost(PostDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<PostData> result = new FacadeUpdateResult<PostData>();
            IPostService service = UnitOfWork.GetService<IPostService>();
            Post instance = RetrieveOrNew<PostData, Post, IPostService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Title = dto.Title;
                instance.Content = dto.Content;
                instance.TopicId = dto.TopicId;
                instance.IssuedById = dto.IssuedById;
                instance.Attachment = dto.Attachment;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<PostData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<PostData> DeletePost(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<PostData> result = new FacadeUpdateResult<PostData>();
            IPostService service = UnitOfWork.GetService<IPostService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Post instance = query.ToBo<Post>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "PostCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IPostService service = UnitOfWork.GetService<IPostService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (PostData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Title));
                }
            }

            return dataSource;
        }
    }
}
