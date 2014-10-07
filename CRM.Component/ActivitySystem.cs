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
    internal class ActivitySystem : BusinessComponent
    {
        public ActivitySystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllActivity<TDto>(IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IActivityService service = UnitOfWork.GetService<IActivityService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveActivitysByEmployee<TDto>(object instanceId, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IActivityService service = UnitOfWork.GetService<IActivityService>();

            var query = service.SearchByEmployee(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveActivitysByCustomer<TDto>(object instanceId, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IActivityService service = UnitOfWork.GetService<IActivityService>();

            var query = service.SearchByCustomer(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveActivitysByContact<TDto>(object instanceId, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IActivityService service = UnitOfWork.GetService<IActivityService>();

            var query = service.SearchByContact(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewActivity<TDto>(object instanceId, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IActivityService service = UnitOfWork.GetService<IActivityService>();
            FacadeUpdateResult<ActivityData> result = new FacadeUpdateResult<ActivityData>();
            Activity instance = RetrieveOrNew<ActivityData, Activity, IActivityService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<ActivityData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ActivityData> SaveActivity(ActivityDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ActivityData> result = new FacadeUpdateResult<ActivityData>();
            IActivityService service = UnitOfWork.GetService<IActivityService>();
            Activity instance = RetrieveOrNew<ActivityData, Activity, IActivityService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.ActivityName = dto.ActivityName;
                instance.EmployeeId = dto.EmployeeId;
                instance.ContactId = dto.ContactId;
                instance.CustomerId = dto.CustomerId;
                instance.ActivityTypeId = dto.ActivityTypeId;
                instance.Notes = dto.Notes;
                instance.StartTime = dto.StartTime;
                instance.EndTime = dto.EndTime;
                instance.TimeSpent = dto.TimeSpent;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ActivityData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<ActivityData> DeleteActivity(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<ActivityData> result = new FacadeUpdateResult<ActivityData>();
            IActivityService service = UnitOfWork.GetService<IActivityService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Activity instance = query.ToBo<Activity>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ActivityCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IActivityService service = UnitOfWork.GetService<IActivityService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (ActivityData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.ActivityName));
                }
            }

            return dataSource;
        }
    }
}
