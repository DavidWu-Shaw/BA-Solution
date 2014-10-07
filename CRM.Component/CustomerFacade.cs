using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class CustomerFacade : BusinessComponent
    {
        public CustomerFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            CustomerSystem = new CustomerSystem(unitOfWork);
        }

        private CustomerSystem CustomerSystem { get; set; }

        public List<TDto> RetrieveAllCustomer<TDto>(IDataConverter<CustomerData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = CustomerSystem.RetrieveAllCustomer(converter);

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewCustomer<TDto>(object id, IDataConverter<CustomerData, TDto> converter)
            where TDto : class
        {
            return CustomerSystem.RetrieveOrNewCustomer(id, converter);
        }

        public IFacadeUpdateResult<CustomerData> SaveCustomer(CustomerDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<CustomerData> result = CustomerSystem.SaveCustomer(dto);
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

        public IFacadeUpdateResult<CustomerData> DeleteCustomer(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<CustomerData> result = CustomerSystem.DeleteCustomer(id);
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

        public IList<BindingListItem> GetBindingList()
        {
            return CustomerSystem.GetBindingList();
        }
    }
}
