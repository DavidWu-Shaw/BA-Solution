using CRM.Data;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service.Contract
{
    public interface ITransactionService : IUpdateEntityService<TransactionData>
    {
        IServiceQueryResultList<TransactionData> SearchByCustomer(object customerId);
        IServiceQueryResultList<TransactionData> SearchByContact(object id);
    }
}
