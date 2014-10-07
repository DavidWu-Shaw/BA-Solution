using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;
using CRM.Data.Criterias;
using CRM.Core;

namespace CRM.Repository
{
    public class ReviewRepository : NHUpdateEntityRepository<ReviewData>, IReviewRepository
    {
        public IEnumerable<ReviewData> SearchByProduct(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<ReviewData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<ReviewData>();
                query.AddExpressionEq<ReviewData, object>(o => o.ObjectId, id);
                query.AddExpressionEq<ReviewData, int>(o => o.ObjectTypeId, (int)ReviewObjectTypes.Product);

                result = query.Future<ReviewData>();
            });

            return result;
        }

        public IEnumerable<ReviewInfoData> SearchInfosByProduct(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<ReviewInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetReviewInfoList");
                query.SetParameter("ObjectId", id);
                query.SetInt32("ObjectTypeId", (int)ReviewObjectTypes.Product);

                result = query.List<ReviewInfoData>();
            });

            return result;
        }

        public IEnumerable<ReviewInfoData> SearchInfosBySupplier(object id)
        {
            ArgumentValidator.IsNotNull("id", id);

            IEnumerable<ReviewInfoData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                IQuery query = CurrentSession.GetNamedQuery("GetReviewInfoList");
                query.SetParameter("ObjectId", id);
                query.SetInt32("ObjectTypeId", (int)ReviewObjectTypes.Supplier);

                result = query.List<ReviewInfoData>();
            });

            return result;
        }
    }
}
