using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using System.Collections.Generic;
using CRM.Data.Criterias;

namespace CRM.Service
{
    public class ProductService : UpdateEntityService<IProductRepository, ProductData>, IProductService
    {
        public IServiceQueryResult<ProductInfoData> RetrieveProductInfo(object productId)
        {
            ProductInfoData result = Repository.RetrieveProductInfo(productId);
            return ServiceResultFactory.BuildServiceQueryResult<ProductInfoData>(result);

        }
        public IServiceQueryResultList<ProductInfoData> Search(ProductCriteria criteria)
        {
            IEnumerable<ProductInfoData> result = Repository.Search(criteria);
            return ServiceResultFactory.BuildServiceQueryResult<ProductInfoData>(result);
        }

        public IServiceQueryResultList<ProductData> SearchBySupplier(object supplierId)
        {
            IEnumerable<ProductData> result = Repository.SearchBySupplier(supplierId);
            return ServiceResultFactory.BuildServiceQueryResult<ProductData>(result);
        }
    }
}
