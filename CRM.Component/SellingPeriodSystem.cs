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
    internal class SellingPeriodSystem : BusinessComponent
    {
        public SellingPeriodSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllSellingPeriod<TDto>(IDataConverter<SellingPeriodData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ISellingPeriodService service = UnitOfWork.GetService<ISellingPeriodService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewSellingPeriod<TDto>(object instanceId, IDataConverter<SellingPeriodData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            ISellingPeriodService service = UnitOfWork.GetService<ISellingPeriodService>();
            FacadeUpdateResult<SellingPeriodData> result = new FacadeUpdateResult<SellingPeriodData>();
            SellingPeriod instance = RetrieveOrNew<SellingPeriodData, SellingPeriod, ISellingPeriodService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<SellingPeriodData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<SellingPeriodData> SaveSellingPeriod(SellingPeriodDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<SellingPeriodData> result = new FacadeUpdateResult<SellingPeriodData>();
            ISellingPeriodService service = UnitOfWork.GetService<ISellingPeriodService>();
            SellingPeriod instance = RetrieveOrNew<SellingPeriodData, SellingPeriod, ISellingPeriodService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.StartTime = dto.StartTime;
                instance.EndTime = dto.EndTime;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<SellingPeriodData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<SellingPeriodData> DeleteSellingPeriod(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<SellingPeriodData> result = new FacadeUpdateResult<SellingPeriodData>();
            ISellingPeriodService service = UnitOfWork.GetService<ISellingPeriodService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                SellingPeriod instance = query.ToBo<SellingPeriod>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "SellingPeriodCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            ISellingPeriodService service = UnitOfWork.GetService<ISellingPeriodService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (SellingPeriodData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
