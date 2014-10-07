using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class CategoryFacade : BusinessComponent
    {
        public CategoryFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            CategorySystem = new CategorySystem(unitOfWork);
        }

        private CategorySystem CategorySystem { get; set; }

        public IEnumerable<TDto> RetrieveAllCategory<TDto>(IDataConverter<CategoryData, TDto> converter)
            where TDto : class
        {
            IEnumerable<TDto> instances = CategorySystem.RetrieveAllCategory(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public IEnumerable<TDto> RetrieveCategoryTree<TDto>(object catalogId, IDataConverter<CategoryData, TDto> converter)
            where TDto : class
        {
            IEnumerable<TDto> instances = CategorySystem.RetrieveCategoryTree(catalogId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewCategory<TDto>(object id, IDataConverter<CategoryData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto instance = CategorySystem.RetrieveOrNewCategory(id, converter);
            UnitOfWork.CommitTransaction();
            return instance;
        }

        public IFacadeUpdateResult<CategoryData> SaveCategory(CategoryDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<CategoryData> result = CategorySystem.SaveCategory(dto);
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

        public IFacadeUpdateResult<CategoryData> DeleteCategory(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<CategoryData> result = CategorySystem.DeleteCategory(id);
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
    }
}
