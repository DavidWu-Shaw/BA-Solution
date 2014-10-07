using CRM.Data;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface IProductService : IUpdateEntityService<ProductData>
    {
        IServiceQueryResult<ProductInfoData> RetrieveProductInfo(object productId);
        IServiceQueryResultList<ProductInfoData> Search(ProductCriteria criteria);
        IServiceQueryResultList<ProductData> SearchBySupplier(object id);
    }
}
