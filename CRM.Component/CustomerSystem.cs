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
    internal class CustomerSystem : BusinessComponent
    {
        public CustomerSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllCustomer<TDto>(IDataConverter<CustomerData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ICustomerService service = UnitOfWork.GetService<ICustomerService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewCustomer<TDto>(object instanceId, IDataConverter<CustomerData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ICustomerService service = UnitOfWork.GetService<ICustomerService>();
            FacadeUpdateResult<CustomerData> result = new FacadeUpdateResult<CustomerData>();
            Customer instance = RetrieveOrNew<CustomerData, Customer, ICustomerService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<CustomerData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<CustomerData> SaveCustomer(CustomerDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<CustomerData> result = new FacadeUpdateResult<CustomerData>();
            ICustomerService service = UnitOfWork.GetService<ICustomerService>();
            Customer instance = RetrieveOrNew<CustomerData, Customer, ICustomerService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.Description = dto.Description;
                instance.AddressLine1 = dto.AddressLine1;
                instance.AddressLine2 = dto.AddressLine2;
                instance.Country = dto.Country;
                instance.CountryState = dto.CountryState;
                instance.City = dto.City;
                instance.ZipCode = dto.ZipCode;
                instance.Phone = dto.Phone;
                instance.Fax = dto.Fax;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<CustomerData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<CustomerData> DeleteCustomer(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<CustomerData> result = new FacadeUpdateResult<CustomerData>();
            ICustomerService service = UnitOfWork.GetService<ICustomerService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Customer instance = query.ToBo<Customer>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "CustomerCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ICustomerService service = UnitOfWork.GetService<ICustomerService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (CustomerData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
