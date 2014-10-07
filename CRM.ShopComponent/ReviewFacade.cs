using System.Collections.Generic;
using Framework.Component;
using CRM.Core;
using CRM.Data;
using CRM.ShopComponent.Dto;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class ReviewFacade : BusinessComponent
    {
        public ReviewFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ReviewSystem = new ReviewSystem(unitOfWork);
        }

        private ReviewSystem ReviewSystem { get; set; }

        public List<TDto> RetrieveReviewsByProduct<TDto>(object id, IDataConverter<ReviewInfoData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ReviewSystem.RetrieveReviewsByProduct(id, converter);

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveReviewsBySupplier<TDto>(object id, IDataConverter<ReviewInfoData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ReviewSystem.RetrieveReviewsBySupplier(id, converter);

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public IFacadeUpdateResult<ReviewData> SaveProductReview(ReviewInfoDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ReviewData> result = ReviewSystem.SaveReview(dto, ReviewObjectTypes.Product);
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

        public IFacadeUpdateResult<ReviewData> SaveSupplierReview(ReviewInfoDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ReviewData> result = ReviewSystem.SaveReview(dto, ReviewObjectTypes.Supplier);
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
