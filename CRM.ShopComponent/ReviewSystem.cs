using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.Business;
using CRM.Core;
using CRM.Data;
using CRM.Service.Contract;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    internal class ReviewSystem : BusinessComponent
    {
        public ReviewSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveReviewsByProduct<TDto>(object productId, IDataConverter<ReviewInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("productId", productId);
            ArgumentValidator.IsNotNull("converter", converter);

            IReviewService service = UnitOfWork.GetService<IReviewService>();
            var query = service.SearchInfosByProduct(productId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveReviewsBySupplier<TDto>(object supplierId, IDataConverter<ReviewInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("supplierId", supplierId);
            ArgumentValidator.IsNotNull("converter", converter);

            IReviewService service = UnitOfWork.GetService<IReviewService>();
            var query = service.SearchInfosBySupplier(supplierId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal IFacadeUpdateResult<ReviewData> SaveReview(ReviewInfoDto dto, ReviewObjectTypes type)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ReviewData> result = new FacadeUpdateResult<ReviewData>();
            IReviewService service = UnitOfWork.GetService<IReviewService>();
            Review instance = RetrieveOrNew<ReviewData, Review, IReviewService>(result.ValidationResult, dto.ReviewId);

            if (result.IsSuccessful)
            {
                instance.ObjectTypeId = (int)type;
                instance.ObjectId = dto.ObjectId;
                instance.Rating = dto.Rating;
                instance.Title = dto.Title;
                instance.Content = dto.Content;
                instance.IssuedById = dto.IssuedById;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ReviewData>());
                result.Merge(saveQuery);
            }

            return result;
        }
    }
}
