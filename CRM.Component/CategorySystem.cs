using System.Collections.Generic;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class CategorySystem : BusinessComponent
    {
        public CategorySystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllCategory<TDto>(IDataConverter<CategoryData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ICategoryService service = UnitOfWork.GetService<ICategoryService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal IEnumerable<TDto> RetrieveCategoryTree<TDto>(object instanceId, IDataConverter<CategoryData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            ICategoryService service = UnitOfWork.GetService<ICategoryService>();

            var query = service.RetrieveCategoryTree(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal TDto RetrieveOrNewCategory<TDto>(object instanceId, IDataConverter<CategoryData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ICategoryService service = UnitOfWork.GetService<ICategoryService>();
            FacadeUpdateResult<CategoryData> result = new FacadeUpdateResult<CategoryData>();
            Category instance = RetrieveOrNew<CategoryData, Category, ICategoryService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<CategoryData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<CategoryData> SaveCategory(CategoryDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<CategoryData> result = new FacadeUpdateResult<CategoryData>();
            ICategoryService service = UnitOfWork.GetService<ICategoryService>();
            Category instance = RetrieveOrNew<CategoryData, Category, ICategoryService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.ParentId = dto.ParentId;
                instance.CatalogId = dto.CatalogId;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<CategoryData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<CategoryData> DeleteCategory(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<CategoryData> result = new FacadeUpdateResult<CategoryData>();
            ICategoryService service = UnitOfWork.GetService<ICategoryService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Category instance = query.ToBo<Category>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "CategoryCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ICategoryService service = UnitOfWork.GetService<ICategoryService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (CategoryData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
