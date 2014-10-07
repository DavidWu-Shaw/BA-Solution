using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class ProductSystem : BusinessComponent
    {
        public ProductSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllProduct<TDto>(IDataConverter<ProductData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IProductService service = UnitOfWork.GetService<IProductService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveProductsBySupplier<TDto>(object supplierId, IDataConverter<ProductData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("supplierId", supplierId);
            ArgumentValidator.IsNotNull("converter", converter);

            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.SearchBySupplier(supplierId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewProduct<TDto>(object instanceId, IDataConverter<ProductData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IProductService service = UnitOfWork.GetService<IProductService>();
            FacadeUpdateResult<ProductData> result = new FacadeUpdateResult<ProductData>();
            Product instance = RetrieveOrNew<ProductData, Product, IProductService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<ProductData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ProductData> SaveProduct(ProductDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ProductData> result = new FacadeUpdateResult<ProductData>();
            IProductService service = UnitOfWork.GetService<IProductService>();
            Product instance = RetrieveOrNew<ProductData, Product, IProductService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                TransferData(dto, instance);
                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ProductData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<ProductData> DeleteProduct(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<ProductData> result = new FacadeUpdateResult<ProductData>();
            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Product instance = query.ToBo<Product>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ProductCannotBeFound");
            }

            return result;
        }

        private void TransferData(ProductDto dto, Product instance)
        {
            instance.Name = dto.Name;
            instance.Description = dto.Description;
            instance.CategoryId = dto.CategoryId;
            instance.SupplierId = dto.SupplierId;
            instance.UnitPrice = dto.UnitPrice;
            instance.Sketch = dto.Sketch;
            instance.SellingPeriodId = dto.SellingPeriodId;
            instance.IsDiscontinued = dto.IsDiscontinued;
            instance.DiscountAmount = dto.DiscountAmount;
            instance.Packaging = dto.Packaging;
            instance.UnitOfMeasure = dto.UnitOfMeasure;
        }

        internal FacadeUpdateResult<ProductData> SaveProducts(List<ProductDto> dtoList)
        {
            ArgumentValidator.IsNotNull("dtoList", dtoList);

            FacadeUpdateResult<ProductData> result = new FacadeUpdateResult<ProductData>();

            List<Product> instances = new List<Product>();
            foreach (ProductDto dto in dtoList)
            {
                Product instance = RetrieveOrNew<ProductData, Product, IProductService>(result.ValidationResult, dto.Id);
                if (!result.IsSuccessful)
                {
                    break;
                }
                TransferData(dto, instance);
                instances.Add(instance);
            }

            if (result.IsSuccessful)
            {
                IProductService service = UnitOfWork.GetService<IProductService>();

                var saveQuery = service.SaveAll(instances);
                result.Merge(saveQuery);

                List<ProductData> dataList = new List<ProductData>();
                foreach (Product instance in instances)
                {
                    dataList.Add(instance.RetrieveData<ProductData>());
                }
                result.AttachResult(dataList);
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IProductService service = UnitOfWork.GetService<IProductService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (ProductData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
