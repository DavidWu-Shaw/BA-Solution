using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class ProductFacade : BusinessComponent
    {
        public ProductFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ProductSystem = new ProductSystem(unitOfWork);
        }

        private ProductSystem ProductSystem { get; set; }

        public List<TDto> RetrieveAllProduct<TDto>(IDataConverter<ProductData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ProductSystem.RetrieveAllProduct(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveProductsBySupplier<TDto>(object supplierId, IDataConverter<ProductData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ProductSystem.RetrieveProductsBySupplier(supplierId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewProduct<TDto>(object instanceId, IDataConverter<ProductData, TDto> converter)
            where TDto : class
        {
            return ProductSystem.RetrieveOrNewProduct(instanceId, converter);
        }

        public IFacadeUpdateResult<ProductData> SaveProduct(ProductDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ProductData> result = ProductSystem.SaveProduct(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<ProductData> DeleteProduct(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ProductData> result = ProductSystem.DeleteProduct(id);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<ProductData> SaveProducts(List<ProductDto> dtoList)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ProductData> result = ProductSystem.SaveProducts(dtoList);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IList<BindingListItem> GetBindingList()
        {
            return ProductSystem.GetBindingList();
        }
    }
}
