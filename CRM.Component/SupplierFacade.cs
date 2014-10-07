using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;
using Framework.Core;
using Framework.Component;

namespace CRM.Component
{
    public class SupplierFacade : BusinessComponent
    {
        public SupplierFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            SupplierSystem = new SupplierSystem(unitOfWork);
        }

        private SupplierSystem SupplierSystem { get; set; }

        public List<TDto> RetrieveAllSupplier<TDto>(IDataConverter<SupplierData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = SupplierSystem.RetrieveAllSupplier(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewSupplier<TDto>(object instanceId, IDataConverter<SupplierData, TDto> converter)
            where TDto : class
        {
            return SupplierSystem.RetrieveOrNewSupplier(instanceId, converter);
        }

        public IFacadeUpdateResult<SupplierData> SaveSupplier(SupplierDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SupplierData> result = SupplierSystem.SaveSupplier(dto);
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

        public IFacadeUpdateResult<SupplierData> DeleteSupplier(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SupplierData> result = SupplierSystem.DeleteSupplier(id);
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

        public IFacadeUpdateResult<SupplierData> SaveSuppliers(List<SupplierDto> dtoList)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SupplierData> result = SupplierSystem.SaveSuppliers(dtoList);
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
            return SupplierSystem.GetBindingList();
        }
    }
}
