using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class TransactionSystem : BusinessComponent
    {
        public TransactionSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllTransaction<TDto>(IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveTransactionsByCustomer<TDto>(object instanceId, IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();

            var query = service.SearchByCustomer(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveTransactionsByContact<TDto>(object instanceId, IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();

            var query = service.SearchByContact(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewTransaction<TDto>(object instanceId, IDataConverter<TransactionData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();
            FacadeUpdateResult<TransactionData> result = new FacadeUpdateResult<TransactionData>();
            Transaction instance = RetrieveOrNew<TransactionData, Transaction, ITransactionService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<TransactionData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<TransactionData> SaveTransaction(TransactionDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<TransactionData> result = new FacadeUpdateResult<TransactionData>();
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();
            Transaction instance = RetrieveOrNew<TransactionData, Transaction, ITransactionService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.TransactionNumber = dto.TransactionNumber;
                instance.CustomerId = dto.CustomerId;
                instance.ContactId = dto.ContactId;
                instance.DateSigned = dto.DateSigned;
                instance.EffectiveDate = dto.EffectiveDate;
                instance.Amount = dto.Amount;
                instance.CurrencyId = dto.CurrencyId;
                instance.Notes = dto.Notes;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<TransactionData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<TransactionData> DeleteTransaction(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<TransactionData> result = new FacadeUpdateResult<TransactionData>();
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Transaction instance = query.ToBo<Transaction>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "TransactionCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<TransactionData> DeleteTransactionItem(object parentId, object childId)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childId", childId);

            FacadeUpdateResult<TransactionData> result = new FacadeUpdateResult<TransactionData>();
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();
            var query = service.Retrieve(parentId);

            if (query.HasResult)
            {
                Transaction parent = query.ToBo<Transaction>();
                TransactionItem child = parent.TransactionItems.SingleOrDefault(o => object.Equals(o.Id, childId));
                if (child != null)
                {
                    parent.TransactionItems.Remove(child);
                    var saveQuery = parent.Save();
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<TransactionData>());
                }
                else
                {
                    AddError(result.ValidationResult, "TransactionItemCannotBeFound");
                }
            }
            else
            {
                AddError(result.ValidationResult, "TransactionCannotBeFound");
            }

            return result;
        }

        internal IFacadeUpdateResult<TransactionData> SaveTransactionItem(object parentId, TransactionItemDto childDto)
        {
            ArgumentValidator.IsNotNull("parentId", parentId);
            ArgumentValidator.IsNotNull("childDto", childDto);

            FacadeUpdateResult<TransactionData> result = new FacadeUpdateResult<TransactionData>();
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();
            var parentQuery = service.Retrieve(parentId);
            if (parentQuery.HasResult)
            {
                Transaction parent = parentQuery.ToBo<Transaction>();
                TransactionItem child = RetrieveOrNewTransactionItem(parent, childDto.Id);
                if (child != null)
                {
                    child.ItemDescription = childDto.ItemDescription;
                    child.ProductId = childDto.ProductId;
                    child.Quantity = childDto.Quantity;
                    child.UnitPrice = childDto.UnitPrice;
                    child.Amount = childDto.Amount;

                    var saveQuery = service.Save(parent);
                    result.Merge(saveQuery);
                    result.AttachResult(parent.RetrieveData<TransactionData>());
                }
                else
                {
                    AddError(result.ValidationResult, "TransactionItemCannotBeFound");
                }
            }

            return result;
        }

        internal TransactionItem RetrieveOrNewTransactionItem(Transaction parent, object childId)
        {
            TransactionItem child = null;

            if (childId != null)
            {
                child = parent.TransactionItems.SingleOrDefault(o => object.Equals(o.Id, childId));
            }
            else
            {
                child = parent.TransactionItems.AddNewBo();
            }

            return child;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ITransactionService service = UnitOfWork.GetService<ITransactionService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (TransactionData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.TransactionNumber));
                }
            }

            return dataSource;
        }
    }
}
