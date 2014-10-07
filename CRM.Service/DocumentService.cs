using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class DocumentService : UpdateEntityService<IDocumentRepository, DocumentData>, IDocumentService
    {
        public IServiceQueryResultList<DocumentData> SearchByUser(object userId)
        {
            IEnumerable<DocumentData> result = Repository.SearchByUser(userId);
            return ServiceResultFactory.BuildServiceQueryResult<DocumentData>(result);
        }
    }
}
