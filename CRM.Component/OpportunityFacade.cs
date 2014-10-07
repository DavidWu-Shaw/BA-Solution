using System.Collections.Generic;
using Framework.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class OpportunityFacade : BusinessComponent
    {
        public OpportunityFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            OpportunitySystem = new OpportunitySystem(unitOfWork);
        }

        private OpportunitySystem OpportunitySystem { get; set; }

        public List<TDto> RetrieveAllOpportunity<TDto>(IDataConverter<OpportunityData, TDto> converter)
            where TDto : class
        {
            return OpportunitySystem.RetrieveAllOpportunity(converter);
        }

        public TDto RetrieveOrNewOpportunity<TDto>(object id, IDataConverter<OpportunityData, TDto> converter)
            where TDto : class
        {
            return OpportunitySystem.RetrieveOrNewOpportunity(id, converter);
        }

        public IFacadeUpdateResult<OpportunityData> SaveOpportunity(OpportunityDto dto)
        {
            return OpportunitySystem.SaveOpportunity(dto);
        }

        public IFacadeUpdateResult<OpportunityData> DeleteOpportunity(object id)
        {
            return OpportunitySystem.DeleteOpportunity(id);
        }

        public IList<BindingListItem> GetBindingList()
        {
            return OpportunitySystem.GetBindingList();
        }
    }
}
