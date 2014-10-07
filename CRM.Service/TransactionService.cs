using System.Collections.Generic;
using CRM.Data;
using CRM.Repository.Contract;
using CRM.Service.Contract;
using Framework.Service;
using Framework.UoW;

namespace CRM.Service
{
    public class TransactionService : UpdateEntityService<ITransactionRepository, TransactionData>, ITransactionService
    {
        public IServiceQueryResultList<TransactionData> SearchByCustomer(object customerId)
        {
            IEnumerable<TransactionData> result = Repository.SearchByCustomer(customerId);
            return ServiceResultFactory.BuildServiceQueryResult<TransactionData>(result);
        }

        public IServiceQueryResultList<TransactionData> SearchByContact(object contactId)
        {
            IEnumerable<TransactionData> result = Repository.SearchByContact(contactId);
            return ServiceResultFactory.BuildServiceQueryResult<TransactionData>(result);
        }
    }
}
