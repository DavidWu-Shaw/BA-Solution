using Framework.Data;
using CRM.Data;
using System.Collections.Generic;
using CRM.Data.Criterias;

namespace CRM.Repository.Contract
{
    public interface IProductRepository : IUpdateEntityRepository<ProductData>
    {
        ProductInfoData RetrieveProductInfo(object productId);
        IEnumerable<ProductInfoData> Search(ProductCriteria criteria);
        IEnumerable<ProductData> SearchBySupplier(object supplierId);
    }
}
