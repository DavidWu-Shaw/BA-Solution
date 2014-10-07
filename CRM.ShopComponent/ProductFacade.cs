using System.Collections.Generic;
using Framework.Component;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.ShopComponent
{
    public class ProductFacade : BusinessComponent
    {
        public ProductFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ProductSystem = new ProductSystem(unitOfWork);
        }

        private ProductSystem ProductSystem { get; set; }

        public List<TDto> RetrieveProductsByCategory<TDto>(object id, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ProductSystem.RetrieveProductsByCategory(id, converter);

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveProductsBySupplier<TDto>(object id, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ProductSystem.RetrieveProductsBySupplier(id, converter);

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveGlobalProducts<TDto>(object catalogId, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ProductSystem.RetrieveGlobalProducts(catalogId, converter);

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveProductInfo<TDto>(object productId, IDataConverter<ProductInfoData, TDto> converter)
            where TDto : class
        {
            TDto instance = ProductSystem.RetrieveProductInfo(productId, converter);
            return instance;
        }
    }
}
