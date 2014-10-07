using System.Collections.Generic;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Component.Converters;
using Setup.Component.Dto;
using Setup.Core;
using Setup.Data;

namespace Setup.Component
{
    public class ApplicationSettingFacade : BusinessComponent
    {
        public ApplicationSettingFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ApplicationSettingSystem = new ApplicationSettingSystem(unitOfWork);
        }

        private ApplicationSettingSystem ApplicationSettingSystem { get; set; }

        public IEnumerable<ApplicationSettingDto> RetrieveAllApplicationSetting()
        {
            IEnumerable<ApplicationSettingDto> instances = ApplicationSettingSystem.RetrieveAllApplicationSetting(new ApplicationSettingConverter());
            if (instances == null)
            {
                instances = new List<ApplicationSettingDto>();
            }
            return instances;
        }

        public ApplicationSettingDto RetrieveOrNewApplicationSetting(object id)
        {
            return ApplicationSettingSystem.RetrieveOrNewApplicationSetting(id, new ApplicationSettingConverter());
        }

        public IFacadeUpdateResult<ApplicationSettingData> SaveApplicationSetting(ApplicationSettingDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ApplicationSettingData> result = ApplicationSettingSystem.SaveApplicationSetting(dto);
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

        public IFacadeUpdateResult<ApplicationSettingData> DeleteApplicationSetting(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ApplicationSettingData> result = ApplicationSettingSystem.DeleteApplicationSetting(id);
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

        public ApplicationOption GetApplicationOption()
        {
            return ApplicationSettingSystem.GetApplicationOption();
        }

        public IList<BindingListItem> GetBindingList()
        {
            return ApplicationSettingSystem.GetBindingList();
        }
    }
}
