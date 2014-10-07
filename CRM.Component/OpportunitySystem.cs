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
    internal class OpportunitySystem : BusinessComponent
    {
        public OpportunitySystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllOpportunity<TDto>(IDataConverter<OpportunityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IOpportunityService service = UnitOfWork.GetService<IOpportunityService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveOpportunitysByCustomer<TDto>(object customerId, IDataConverter<OpportunityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("customerId", customerId);
            ArgumentValidator.IsNotNull("converter", converter);
            IOpportunityService service = UnitOfWork.GetService<IOpportunityService>();

            var query = service.SearchByCustomer(customerId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewOpportunity<TDto>(object instanceId, IDataConverter<OpportunityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IOpportunityService service = UnitOfWork.GetService<IOpportunityService>();
            FacadeUpdateResult<OpportunityData> result = new FacadeUpdateResult<OpportunityData>();
            Opportunity instance = RetrieveOrNew<OpportunityData, Opportunity, IOpportunityService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<OpportunityData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<OpportunityData> SaveOpportunity(OpportunityDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<OpportunityData> result = new FacadeUpdateResult<OpportunityData>();
            IOpportunityService service = UnitOfWork.GetService<IOpportunityService>();
            Opportunity instance = RetrieveOrNew<OpportunityData, Opportunity, IOpportunityService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.Description = dto.Description;
                instance.ContactId = dto.ContactId;
                instance.CustomerId = dto.CustomerId;
                instance.ProductId = dto.ProductId;
                instance.EstimateAmount = dto.EstimateAmount;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<OpportunityData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<OpportunityData> DeleteOpportunity(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<OpportunityData> result = new FacadeUpdateResult<OpportunityData>();
            IOpportunityService service = UnitOfWork.GetService<IOpportunityService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Opportunity instance = query.ToBo<Opportunity>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "OpportunityCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IOpportunityService service = UnitOfWork.GetService<IOpportunityService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (OpportunityData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
