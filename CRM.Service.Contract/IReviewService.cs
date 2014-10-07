using CRM.Data;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface IReviewService : IUpdateEntityService<ReviewData>
    {
        IServiceQueryResultList<ReviewData> SearchByProduct(object id);
        IServiceQueryResultList<ReviewInfoData> SearchInfosByProduct(object id);
        IServiceQueryResultList<ReviewInfoData> SearchInfosBySupplier(object supplierId);
    }
}
