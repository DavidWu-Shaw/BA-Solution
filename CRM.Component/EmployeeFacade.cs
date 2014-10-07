using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class EmployeeFacade : BusinessComponent
    {
        public EmployeeFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            EmployeeSystem = new EmployeeSystem(unitOfWork);
        }

        private EmployeeSystem EmployeeSystem { get; set; }

        public List<TDto> RetrieveAllEmployee<TDto>(IDataConverter<EmployeeData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = EmployeeSystem.RetrieveAllEmployee(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewEmployee<TDto>(object id, IDataConverter<EmployeeData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto Employee = EmployeeSystem.RetrieveOrNewEmployee(id, converter);
            UnitOfWork.CommitTransaction();
            return Employee;
        }

        public IFacadeUpdateResult<EmployeeData> SaveEmployee(EmployeeDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<EmployeeData> result = EmployeeSystem.SaveEmployee(dto);
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

        public IFacadeUpdateResult<EmployeeData> DeleteEmployee(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<EmployeeData> result = EmployeeSystem.DeleteEmployee(id);
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
            return EmployeeSystem.GetBindingList();
        }
    }
}
