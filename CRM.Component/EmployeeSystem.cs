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
    internal class EmployeeSystem : BusinessComponent
    {
        public EmployeeSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllEmployee<TDto>(IDataConverter<EmployeeData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IEmployeeService service = UnitOfWork.GetService<IEmployeeService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewEmployee<TDto>(object instanceId, IDataConverter<EmployeeData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IEmployeeService service = UnitOfWork.GetService<IEmployeeService>();
            FacadeUpdateResult<EmployeeData> result = new FacadeUpdateResult<EmployeeData>();
            Employee instance = RetrieveOrNew<EmployeeData, Employee, IEmployeeService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<EmployeeData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<EmployeeData> SaveEmployee(EmployeeDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<EmployeeData> result = new FacadeUpdateResult<EmployeeData>();
            IEmployeeService service = UnitOfWork.GetService<IEmployeeService>();
            Employee instance = RetrieveOrNew<EmployeeData, Employee, IEmployeeService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.FullName = dto.FullName;
                instance.JobTitle = dto.JobTitle;
                instance.AddressLine1 = dto.AddressLine1;
                instance.AddressLine2 = dto.AddressLine2;
                instance.Country = dto.Country;
                instance.CountryState = dto.CountryState;
                instance.City = dto.City;
                instance.ZipCode = dto.ZipCode;
                instance.Phone = dto.Phone;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<EmployeeData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<EmployeeData> DeleteEmployee(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<EmployeeData> result = new FacadeUpdateResult<EmployeeData>();
            IEmployeeService service = UnitOfWork.GetService<IEmployeeService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Employee instance = query.ToBo<Employee>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "EmployeeCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IEmployeeService service = UnitOfWork.GetService<IEmployeeService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (EmployeeData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
