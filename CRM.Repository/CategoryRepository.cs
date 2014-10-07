using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;
using CRM.Data.Criterias;

namespace CRM.Repository
{
    public class CategoryRepository : NHUpdateEntityRepository<CategoryData>, ICategoryRepository
    {
        public IEnumerable<CategoryData> RetrieveCategoryTree(object catalogId)
        {
            ArgumentValidator.IsNotNull("catalogId", catalogId);

            IEnumerable<CategoryData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<CategoryData>();
                query.AddExpressionEq<CategoryData, object>(o => o.CatalogId, catalogId);

                result = query.Future<CategoryData>();
            });

            return result;
        }
    }
}
