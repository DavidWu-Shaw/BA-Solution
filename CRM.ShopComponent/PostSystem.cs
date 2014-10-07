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
    internal class PostSystem : BusinessComponent
    {
        public PostSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrievePostsBySearch<TDto>(PostCriteria criteria, IDataConverter<PostInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("criteria", criteria);
            ArgumentValidator.IsNotNull("converter", converter);

            IPostService service = UnitOfWork.GetService<IPostService>();

            var query = service.Search(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal IFacadeUpdateResult<PostData> SaveNewPost(PostInfoDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<PostData> result = new FacadeUpdateResult<PostData>();
            IPostService service = UnitOfWork.GetService<IPostService>();
            Post instance = RetrieveOrNew<PostData, Post, IPostService>(result.ValidationResult, dto.PostId);

            if (result.IsSuccessful)
            {
                instance.TopicId = dto.TopicId;
                instance.Title = dto.Title;
                instance.Content = dto.Content;
                instance.IssuedById = dto.IssuedById;
                instance.Attachment = dto.Attachment;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<PostData>());
                result.Merge(saveQuery);
            }

            return result;
        }
    }
}
