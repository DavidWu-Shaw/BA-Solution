using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface IDocumentService : IUpdateEntityService<DocumentData>
    {
        IServiceQueryResultList<DocumentData> SearchByUser(object userId);
    }
}
