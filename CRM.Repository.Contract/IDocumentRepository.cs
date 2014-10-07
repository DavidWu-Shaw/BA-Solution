using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface IDocumentRepository : IUpdateEntityRepository<DocumentData>
    {
        IEnumerable<DocumentData> SearchByUser(object userId);
    }
}
