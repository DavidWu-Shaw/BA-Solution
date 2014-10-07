using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using Framework.Data.NHibernate;
using Framework.Data;
using NHibernate;
using Framework.Core;

namespace CRM.Repository
{
    public class DocumentRepository : NHUpdateEntityRepository<DocumentData>, IDocumentRepository
    {
        public IEnumerable<DocumentData> SearchByUser(object userId)
        {
            ArgumentValidator.IsNotNull("userId", userId);

            IEnumerable<DocumentData> result = null;

            RepositoryExceptionWrapper.Wrap(GetType(), () =>
            {
                ICriteria query = CurrentSession.CreateCriteria<DocumentData>();
                query.AddExpressionEq<DocumentData, object>(o => o.IssuedById, userId);

                result = query.Future<DocumentData>();
            });

            return result;
        }
    }
}
