using CRM.Data;
using Framework.Service;
using Framework.UoW;
using CRM.Data.Criterias;

namespace CRM.Service.Contract
{
    public interface ICategoryService : IUpdateEntityService<CategoryData>
    {
        IServiceQueryResultList<CategoryData> RetrieveCategoryTree(object catalogId);
    }
}
