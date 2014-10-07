using System.Collections.Generic;
using Framework.Data;
using CRM.Data;

namespace CRM.Repository.Contract
{
    public interface ITransactionRepository : IUpdateEntityRepository<TransactionData>
    {
        IEnumerable<TransactionData> SearchByCustomer(object customerId);
        IEnumerable<TransactionData> SearchByContact(object id);
    }
}
