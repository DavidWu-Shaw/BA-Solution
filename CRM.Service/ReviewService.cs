using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service
{
    public class ReviewService : UpdateEntityService<IReviewRepository, ReviewData>, IReviewService
    {
        public IServiceQueryResultList<ReviewData> SearchByProduct(object productId)
        {
            IEnumerable<ReviewData> result = Repository.SearchByProduct(productId);
            return ServiceResultFactory.BuildServiceQueryResult<ReviewData>(result);
        }

        public IServiceQueryResultList<ReviewInfoData> SearchInfosByProduct(object productId)
        {
            IEnumerable<ReviewInfoData> result = Repository.SearchInfosByProduct(productId);
            return ServiceResultFactory.BuildServiceQueryResult<ReviewInfoData>(result);
        }

        public IServiceQueryResultList<ReviewInfoData> SearchInfosBySupplier(object supplierId)
        {
            IEnumerable<ReviewInfoData> result = Repository.SearchInfosBySupplier(supplierId);
            return ServiceResultFactory.BuildServiceQueryResult<ReviewInfoData>(result);
        }
    }
}
