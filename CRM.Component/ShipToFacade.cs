using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class ShipToFacade : BusinessComponent
    {
        public ShipToFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ShipToSystem = new ShipToSystem(unitOfWork);
        }

        private ShipToSystem ShipToSystem { get; set; }

        public List<TDto> RetrieveAllShipTo<TDto>(IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ShipToSystem.RetrieveAllShipTo(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveShipTosByCustomer<TDto>(object instanceId, IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ShipToSystem.RetrieveShipTosByCustomer(instanceId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveShipTosByContactPhone<TDto>(string phone, IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ShipToSystem.RetrieveShipTosByContactPhone(phone, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewShipTo<TDto>(object instanceId, IDataConverter<ShipToData, TDto> converter)
            where TDto : class
        {
            return ShipToSystem.RetrieveOrNewShipTo(instanceId, converter);
        }

        public IFacadeUpdateResult<ShipToData> SaveShipTo(ShipToDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ShipToData> result = ShipToSystem.SaveShipTo(dto);
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

        public IFacadeUpdateResult<ShipToData> DeleteShipTo(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ShipToData> result = ShipToSystem.DeleteShipTo(id);
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
            return ShipToSystem.GetBindingList();
        }
    }
}
