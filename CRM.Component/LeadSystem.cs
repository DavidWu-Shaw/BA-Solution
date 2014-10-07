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
    internal class LeadSystem : BusinessComponent
    {
        public LeadSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllLead<TDto>(IDataConverter<LeadData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ILeadService service = UnitOfWork.GetService<ILeadService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewLead<TDto>(object instanceId, IDataConverter<LeadData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ILeadService service = UnitOfWork.GetService<ILeadService>();
            FacadeUpdateResult<LeadData> result = new FacadeUpdateResult<LeadData>();
            Lead instance = RetrieveOrNew<LeadData, Lead, ILeadService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<LeadData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<LeadData> SaveLead(LeadDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<LeadData> result = new FacadeUpdateResult<LeadData>();
            ILeadService service = UnitOfWork.GetService<ILeadService>();
            Lead instance = RetrieveOrNew<LeadData, Lead, ILeadService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.FullName = dto.FullName;
                instance.FamilyName = dto.FamilyName;
                instance.Gender = dto.Gender;
                instance.AddressLine1 = dto.AddressLine1;
                instance.AddressLine2 = dto.AddressLine2;
                instance.Country = dto.Country;
                instance.CountryState = dto.CountryState;
                instance.City = dto.City;
                instance.ZipCode = dto.ZipCode;
                instance.Phone = dto.Phone;
                instance.Fax = dto.Fax;
                instance.Email = dto.Email;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<LeadData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<LeadData> DeleteLead(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<LeadData> result = new FacadeUpdateResult<LeadData>();
            ILeadService service = UnitOfWork.GetService<ILeadService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Lead instance = query.ToBo<Lead>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "LeadCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ILeadService service = UnitOfWork.GetService<ILeadService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (LeadData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, string.Format("{0} {1}", data.FullName, data.FamilyName)));
                }
            }

            return dataSource;
        }
    }
}
