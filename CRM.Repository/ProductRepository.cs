using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using System.Collections.Generic;
using Framework.Core;
using Framework.Data;
using NHibernate;
using CRM.Data.Criterias;
using NHibernate.Criterion;

namespace CRM.Repository
{
    public class ProductRepository : NHUpdateEntityRepository<ProductData>, IProductRepository
    {
        public ProductInfoData RetrieveProductInfo(object productId)
        {
            ProductInfoData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ProductInfoData>();

                query.AddExpressionEq<ProductInfoData, object>(o => o.ProductId, productId);

                result = query.UniqueResult<ProductInfoData>();
            });

            return result;
        }

        public IEnumerable<ProductInfoData> Search(ProductCriteria criteria)
        {
            ArgumentValidator.IsNotNull("critetia", criteria);

            IEnumerable<ProductInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ProductInfoData>();
                if (criteria.SupplierId != null)
                {
                    query.AddExpressionEq<ProductInfoData, object>(o => o.SupplierId, criteria.SupplierId);
                }
                if (criteria.CategoryId != null)
                {
                    query.AddExpressionEq<ProductInfoData, object>(o => o.CategoryId, criteria.CategoryId);
                }
                if (!string.IsNullOrEmpty(criteria.ProductName))
                {
                    query.AddExpressionInsensitiveLike<ProductInfoData, string>(o => o.Name, criteria.ProductName, MatchMode.Anywhere);
                }
                if (criteria.CatalogId != null)
                {
                    query.AddExpressionEq<ProductInfoData, object>(o => o.CatalogId, criteria.CatalogId);
                }

                result = query.List<ProductInfoData>();
            });

            return result;
        }

        public IEnumerable<ProductData> SearchBySupplier(object supplierId)
        {
            ArgumentValidator.IsNotNull("SupplierId", supplierId);

            IEnumerable<ProductData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ProductData>();
                query.AddExpressionEq<ProductData, object>(o => o.SupplierId, supplierId);

                result = query.Future<ProductData>();
            });

            return result;
        }
    }
}
