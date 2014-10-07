using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class PostFacade : BusinessComponent
    {
        public PostFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            PostSystem = new PostSystem(unitOfWork);
        }

        private PostSystem PostSystem { get; set; }

        public List<TDto> RetrieveAllPost<TDto>(IDataConverter<PostData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = PostSystem.RetrieveAllPost(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrievePostsByTopic<TDto>(object topicId, IDataConverter<PostData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = PostSystem.RetrievePostsByTopic(topicId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewPost<TDto>(object id, IDataConverter<PostData, TDto> converter)
            where TDto : class
        {
            return PostSystem.RetrieveOrNewPost(id, converter);
        }

        public IFacadeUpdateResult<PostData> SavePost(PostDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<PostData> result = PostSystem.SavePost(dto);
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

        public IFacadeUpdateResult<PostData> DeletePost(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<PostData> result = PostSystem.DeletePost(id);
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
