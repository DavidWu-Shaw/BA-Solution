using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using CRM.Data;
using CRM.Data.Criterias;
using CRM.Service.Contract;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    internal class ProductSystem : BusinessComponent
    {
        public ProductSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveProductsByCategory<TDto>(object categoryId, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);

            ProductCriteria criteria = new ProductCriteria();
            criteria.CategoryId = categoryId;
            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.Search(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveProductsBySupplier<TDto>(object supplierId, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("supplierId", supplierId);
            ArgumentValidator.IsNotNull("converter", converter);

            ProductCriteria criteria = new ProductCriteria();
            criteria.SupplierId = supplierId;
            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.Search(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveGlobalProducts<TDto>(object catalogId, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("supplierId", catalogId);
            ArgumentValidator.IsNotNull("converter", converter);

            ProductCriteria criteria = new ProductCriteria();
            criteria.CatalogId = catalogId;
            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.Search(criteria);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveProductInfo<TDto>(object productId, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("productId", productId);
            ArgumentValidator.IsNotNull("converter", converter);

            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.RetrieveProductInfo(productId);

            if (query.HasResult)
            {
                return query.DataToDto(converter);
            }

            return null;
        }

        internal ProductInfoData RetrieveProductInfo(object productId)
        {
            ArgumentValidator.IsNotNull("productId", productId);

            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.RetrieveProductInfo(productId);

            if (query.HasResult)
            {
                return query.Data;
            }

            return null;
        }
    }
}
