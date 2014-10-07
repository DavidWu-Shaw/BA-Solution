using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service
{
    public class CategoryService : UpdateEntityService<ICategoryRepository, CategoryData>, ICategoryService
    {
        public IServiceQueryResultList<CategoryData> RetrieveCategoryTree(object catalogId)
        {
            IEnumerable<CategoryData> result = Repository.RetrieveCategoryTree(catalogId);
            return ServiceResultFactory.BuildServiceQueryResult<CategoryData>(result);
        }
    }
}
