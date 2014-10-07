using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class ActivityFacade : BusinessComponent
    {
        public ActivityFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ActivitySystem = new ActivitySystem(unitOfWork);
        }

        private ActivitySystem ActivitySystem { get; set; }

        public List<TDto> RetrieveAllActivity<TDto>(IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = ActivitySystem.RetrieveAllActivity(converter);
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveActivitysByEmployee<TDto>(object id, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = ActivitySystem.RetrieveActivitysByEmployee(id, converter);
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveActivitysByCustomer<TDto>(object id, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = ActivitySystem.RetrieveActivitysByCustomer(id, converter);
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveActivitysByContact<TDto>(object id, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            List<TDto> instances = ActivitySystem.RetrieveActivitysByContact(id, converter);
            UnitOfWork.CommitTransaction();

            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewActivity<TDto>(object id, IDataConverter<ActivityData, TDto> converter)
            where TDto : class
        {
            UnitOfWork.BeginTransaction();
            TDto instance = ActivitySystem.RetrieveOrNewActivity(id, converter);
            UnitOfWork.CommitTransaction();

            return instance;
        }

        public IFacadeUpdateResult<ActivityData> SaveActivity(ActivityDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ActivityData> result = ActivitySystem.SaveActivity(dto);
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

        public IFacadeUpdateResult<ActivityData> DeleteActivity(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ActivityData> result = ActivitySystem.DeleteActivity(id);
            
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
            UnitOfWork.BeginTransaction();
            IList<BindingListItem> result = ActivitySystem.GetBindingList();
            UnitOfWork.CommitTransaction();

            return result;
        }
    }
}
