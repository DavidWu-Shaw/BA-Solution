using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class LeadFacade : BusinessComponent
    {
        public LeadFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            LeadSystem = new LeadSystem(unitOfWork);
        }

        private LeadSystem LeadSystem { get; set; }

        public List<TDto> RetrieveAllLead<TDto>(IDataConverter<LeadData, TDto> converter)
            where TDto : class
        {
            return LeadSystem.RetrieveAllLead(converter);
        }

        public TDto RetrieveOrNewLead<TDto>(object id, IDataConverter<LeadData, TDto> converter)
            where TDto : class
        {
            return LeadSystem.RetrieveOrNewLead(id, converter);
        }

        public IFacadeUpdateResult<LeadData> SaveLead(LeadDto dto)
        {
            return LeadSystem.SaveLead(dto);
        }

        public IFacadeUpdateResult<LeadData> DeleteLead(object id)
        {
            return LeadSystem.DeleteLead(id);
        }

        public IList<BindingListItem> GetBindingList()
        {
            return LeadSystem.GetBindingList();
        }
    }
}
