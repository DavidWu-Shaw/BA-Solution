using System.Collections.Generic;
using Framework.Component;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class PostFacade : BusinessComponent
    {
        public PostFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            PostSystem = new PostSystem(unitOfWork);
        }

        private PostSystem PostSystem { get; set; }

        public List<TDto> RetrievePostsByTopic<TDto>(object id, IDataConverter<PostInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("id", id);

            PostCriteria criteria = new PostCriteria();
            criteria.TopicId = id;
            List<TDto> instances = PostSystem.RetrievePostsBySearch(criteria, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public IFacadeUpdateResult<PostData> SavePost(PostInfoDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<PostData> result = PostSystem.SaveNewPost(dto);
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
