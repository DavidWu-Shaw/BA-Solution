using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class TransactionFacade : BusinessComponent
    {
        public TransactionFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            TransactionSystem = new TransactionSystem(unitOfWork);
        }

        private TransactionSystem TransactionSystem { get; set; }

        public List<TDto> RetrieveAllTransaction<TDto>(IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = TransactionSystem.RetrieveAllTransaction(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveTransactionsByCustomer<TDto>(object instanceId, IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = TransactionSystem.RetrieveTransactionsByCustomer(instanceId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveTransactionsByContact<TDto>(object instanceId, IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = TransactionSystem.RetrieveTransactionsByContact(instanceId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewTransaction<TDto>(object instanceId, IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            return TransactionSystem.RetrieveOrNewTransaction(instanceId, converter);
        }

        public IFacadeUpdateResult<TransactionData> SaveTransaction(TransactionDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<TransactionData> result = TransactionSystem.SaveTransaction(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<TransactionData> DeleteTransaction(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<TransactionData> result = TransactionSystem.DeleteTransaction(id);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<TransactionData> DeleteTransactionItem(object parentId, object childId)
        {
            return TransactionSystem.DeleteTransactionItem(parentId, childId);
        }

        public IFacadeUpdateResult<TransactionData> SaveTransactionItem(object parentId, TransactionItemDto childDto)
        {
            return TransactionSystem.SaveTransactionItem(parentId, childDto);
        }

        public IList<BindingListItem> GetBindingList()
        {
            return TransactionSystem.GetBindingList();
        }
    }
}
