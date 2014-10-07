using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using System.Collections.Generic;
using CRM.Data.Criterias;
using Framework.Core;
using Framework.Data;
using NHibernate;
using NHibernate.Criterion;

namespace CRM.Repository
{
    public class SupplierRepository : NHUpdateEntityRepository<SupplierData>, ISupplierRepository
    {
        public IEnumerable<SupplierInfoData> Search(SupplierCriteria criteria)
        {
            ArgumentValidator.IsNotNull("critetia", criteria);

            IEnumerable<SupplierInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<SupplierInfoData>();
                if (criteria.CategoryId != null)
                {
                    query.AddExpressionEq<SupplierInfoData, object>(o => o.CategoryId, criteria.CategoryId);
                }
                if (!string.IsNullOrEmpty(criteria.SupplierName))
                {
                    query.AddExpressionInsensitiveLike<SupplierInfoData, string>(o => o.Name, criteria.SupplierName, MatchMode.Anywhere);
                }
                if (!string.IsNullOrEmpty(criteria.ZipCode))
                {
                    query.AddExpressionInsensitiveLike<SupplierInfoData, string>(o => o.ZipCode, criteria.ZipCode, MatchMode.Anywhere);
                }
                if (!string.IsNullOrEmpty(criteria.Address))
                {
                    query.AddExpressionInsensitiveLike<SupplierInfoData, string>(o => o.Address, criteria.Address, MatchMode.Anywhere);
                }

                result = query.List<SupplierInfoData>();
            });

            return result;
        }

        public SupplierInfoData RetrieveSupplierInfo(object supplierId)
        {
            SupplierInfoData result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<SupplierInfoData>();

                query.AddExpressionEq<SupplierInfoData, object>(o => o.SupplierId, supplierId);

                result = query.UniqueResult<SupplierInfoData>();
            });

            return result;
        }
    }
}
