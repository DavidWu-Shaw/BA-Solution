using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class ReviewFacade : BusinessComponent
    {
        public ReviewFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ReviewSystem = new ReviewSystem(unitOfWork);
        }

        private ReviewSystem ReviewSystem { get; set; }

        public List<TDto> RetrieveAllReview<TDto>(IDataConverter<ReviewData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ReviewSystem.RetrieveAllReview(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveReviewsByProduct<TDto>(object productId, IDataConverter<ReviewData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ReviewSystem.RetrieveReviewsByProduct(productId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewReview<TDto>(object instanceId, IDataConverter<ReviewData, TDto> converter)
            where TDto : class
        {
            return ReviewSystem.RetrieveOrNewReview(instanceId, converter);
        }

        public IFacadeUpdateResult<ReviewData> SaveReview(ReviewDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ReviewData> result = ReviewSystem.SaveReview(dto);
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

        public IFacadeUpdateResult<ReviewData> DeleteReview(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ReviewData> result = ReviewSystem.DeleteReview(id);
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
