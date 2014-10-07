using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class SellingPeriodFacade : BusinessComponent
    {
        public SellingPeriodFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            SellingPeriodSystem = new SellingPeriodSystem(unitOfWork);
        }

        private SellingPeriodSystem SellingPeriodSystem { get; set; }

        public List<TDto> RetrieveAllSellingPeriod<TDto>(IDataConverter<SellingPeriodData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = SellingPeriodSystem.RetrieveAllSellingPeriod(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewSellingPeriod<TDto>(object instanceId, IDataConverter<SellingPeriodData, TDto> converter)
            where TDto : class
        {
            return SellingPeriodSystem.RetrieveOrNewSellingPeriod(instanceId, converter);
        }

        public IFacadeUpdateResult<SellingPeriodData> SaveSellingPeriod(SellingPeriodDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SellingPeriodData> result = SellingPeriodSystem.SaveSellingPeriod(dto);
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

        public IFacadeUpdateResult<SellingPeriodData> DeleteSellingPeriod(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<SellingPeriodData> result = SellingPeriodSystem.DeleteSellingPeriod(id);
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
            return SellingPeriodSystem.GetBindingList();
        }
    }
}
