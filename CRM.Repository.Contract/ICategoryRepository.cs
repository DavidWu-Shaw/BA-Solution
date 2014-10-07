using System.Collections.Generic;
using Framework.Data;
using CRM.Data;
using CRM.Data.Criterias;

namespace CRM.Repository.Contract
{
    public interface ICategoryRepository : IUpdateEntityRepository<CategoryData>
    {
        IEnumerable<CategoryData> RetrieveCategoryTree(object catalogId);
    }
}
