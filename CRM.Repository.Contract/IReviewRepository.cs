using System.Collections.Generic;
using Framework.Data;
using CRM.Data;
using CRM.Data.Criterias;

namespace CRM.Repository.Contract
{
    public interface IReviewRepository : IUpdateEntityRepository<ReviewData>
    {
        IEnumerable<ReviewData> SearchByProduct(object id);
        IEnumerable<ReviewInfoData> SearchInfosByProduct(object id);
        IEnumerable<ReviewInfoData> SearchInfosBySupplier(object id);
    }
}
