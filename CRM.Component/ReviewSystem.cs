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
    internal class ReviewSystem : BusinessComponent
    {
        public ReviewSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllReview<TDto>(IDataConverter<ReviewData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IReviewService service = UnitOfWork.GetService<IReviewService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveReviewsByProduct<TDto>(object productId, IDataConverter<ReviewData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("productId", productId);
            ArgumentValidator.IsNotNull("converter", converter);

            IReviewService service = UnitOfWork.GetService<IReviewService>();
            var query = service.SearchByProduct(productId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewReview<TDto>(object instanceId, IDataConverter<ReviewData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IReviewService service = UnitOfWork.GetService<IReviewService>();
            FacadeUpdateResult<ReviewData> result = new FacadeUpdateResult<ReviewData>();
            Review instance = RetrieveOrNew<ReviewData, Review, IReviewService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<ReviewData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ReviewData> SaveReview(ReviewDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ReviewData> result = new FacadeUpdateResult<ReviewData>();
            IReviewService service = UnitOfWork.GetService<IReviewService>();
            Review instance = RetrieveOrNew<ReviewData, Review, IReviewService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Title = dto.Title;
                instance.Content = dto.Content;
                instance.IssuedById = dto.IssuedById;
                instance.IssuedTime = dto.IssuedTime;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ReviewData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<ReviewData> DeleteReview(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<ReviewData> result = new FacadeUpdateResult<ReviewData>();
            IReviewService service = UnitOfWork.GetService<IReviewService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Review instance = query.ToBo<Review>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ReviewCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IReviewService service = UnitOfWork.GetService<IReviewService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (ReviewData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Title));
                }
            }

            return dataSource;
        }
    }
}
