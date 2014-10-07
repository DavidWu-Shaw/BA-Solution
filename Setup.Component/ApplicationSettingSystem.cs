using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Component;
using Framework.Core;
using Framework.UoW;
using Setup.Business;
using Setup.Component.Dto;
using Setup.Core;
using Setup.Data;
using Setup.Service.Contract;

namespace Setup.Component
{
    internal class ApplicationSettingSystem : BusinessComponent
    {
        public ApplicationSettingSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal IEnumerable<TDto> RetrieveAllApplicationSetting<TDto>(IDataConverter<ApplicationSettingData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IApplicationSettingService service = UnitOfWork.GetService<IApplicationSettingService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter);
            }

            return null;
        }

        internal TDto RetrieveOrNewApplicationSetting<TDto>(object instanceId, IDataConverter<ApplicationSettingData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IApplicationSettingService service = UnitOfWork.GetService<IApplicationSettingService>();
            FacadeUpdateResult<ApplicationSettingData> result = new FacadeUpdateResult<ApplicationSettingData>();
            ApplicationSetting instance = RetrieveOrNew<ApplicationSettingData, ApplicationSetting, IApplicationSettingService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<ApplicationSettingData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ApplicationSettingData> SaveApplicationSetting(ApplicationSettingDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ApplicationSettingData> result = new FacadeUpdateResult<ApplicationSettingData>();
            IApplicationSettingService service = UnitOfWork.GetService<IApplicationSettingService>();
            ApplicationSetting instance = RetrieveOrNew<ApplicationSettingData, ApplicationSetting, IApplicationSettingService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.SettingKey = dto.SettingKey;
                instance.SettingValue = dto.SettingValue;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ApplicationSettingData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<ApplicationSettingData> DeleteApplicationSetting(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<ApplicationSettingData> result = new FacadeUpdateResult<ApplicationSettingData>();
            IApplicationSettingService service = UnitOfWork.GetService<IApplicationSettingService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                ApplicationSetting instance = query.ToBo<ApplicationSetting>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ApplicationSettingCannotBeFound");
            }

            return result;
        }

        internal ApplicationOption GetApplicationOption()
        {
            ApplicationOption option = new ApplicationOption();
            IApplicationSettingService service = UnitOfWork.GetService<IApplicationSettingService>();
            var query = service.GetAll();

            if (query.HasResult)
            {
                ParseSettings(option, query.DataList);
            }

            return option;
        }

        private void ParseSettings(ApplicationOption option, IEnumerable<ApplicationSettingData> settings)
        {
            ApplicationSettingData setting;

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.Name.ToString()));
            if (setting != null)
            {
                option.Name = setting.SettingValue;
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.Version.ToString()));
            if (setting != null)
            {
                option.Version = setting.SettingValue;
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsTestMode.ToString()));
            if (setting != null)
            {
                try { option.IsTestMode = Convert.ToBoolean(setting.SettingValue); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.EnableSSL.ToString()));
            if (setting != null)
            {
                try { option.EnableSSL = Convert.ToBoolean(setting.SettingValue); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.EnableMultiUser.ToString()));
            if (setting != null)
            {
                try
                {
                    option.EnableMultiUser = Convert.ToBoolean(setting.SettingValue);
                }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.EnableGridColumnFilter.ToString()));
            if (setting != null)
            {
                try { option.EnableGridColumnFilter = Convert.ToBoolean(setting.SettingValue); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.EnableNotification.ToString()));
            if (setting != null)
            {
                try { option.EnableNotification = Convert.ToBoolean(setting.SettingValue); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.SchedulerDefaultView.ToString()));
            if (setting != null)
            {
                try { option.SchedulerDefaultView = (SchedulerViewTypes)Enum.Parse(typeof(SchedulerViewTypes), setting.SettingValue.Trim()); }
                catch { }
            }
            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DayStartTime.ToString()));
            if (setting != null)
            {
                try { option.DayStartTime = TimeSpan.Parse(setting.SettingValue.Trim()); }
                catch { }
            }
            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DayEndTime.ToString()));
            if (setting != null)
            {
                try { option.DayEndTime = TimeSpan.Parse(setting.SettingValue.Trim()); }
                catch { }
            }
            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DropDownHeight.ToString()));
            if (setting != null)
            {
                try { option.DropDownHeight = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }
            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.GridPageSize.ToString()));
            if (setting != null)
            {
                try { option.GridPageSize = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }
            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.EditFormColumnMax.ToString()));
            if (setting != null)
            {
                try { option.EditFormColumnMax = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.NewsContentBriefLength.ToString()));
            if (setting != null)
            {
                try { option.NewsContentBriefLength = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DateFormat.ToString()));
            if (setting != null)
            {
                try { option.DateFormat = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DateTimeFormat.ToString()));
            if (setting != null)
            {
                try { option.DateTimeFormat = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.TimeFormat.ToString()));
            if (setting != null)
            {
                try { option.TimeFormat = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DateFormatString.ToString()));
            if (setting != null)
            {
                try { option.DateFormatString = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DateTimeFormatString.ToString()));
            if (setting != null)
            {
                try { option.DateTimeFormatString = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.TimeFormatString.ToString()));
            if (setting != null)
            {
                try { option.TimeFormatString = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.SMTPServer.ToString()));
            if (setting != null)
            {
                try { option.SMTPServer = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.SMTPPort.ToString()));
            if (setting != null)
            {
                try { option.SMTPPort = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.SMTPUsername.ToString()));
            if (setting != null)
            {
                try { option.SMTPUsername = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.SMTPPassword.ToString()));
            if (setting != null)
            {
                try { option.SMTPPassword = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.OrderCoordinatorEmail.ToString()));
            if (setting != null)
            {
                try { option.OrderCoordinatorEmail = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DefaultEmailFrom.ToString()));
            if (setting != null)
            {
                try { option.DefaultEmailFrom = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.OrderTrackingUrlFormat.ToString()));
            if (setting != null)
            {
                try { option.OrderTrackingUrlFormat = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsEmailToCustomer.ToString()));
            if (setting != null)
            {
                try { option.IsEmailToCustomer = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsSetSOHold.ToString()));
            if (setting != null)
            {
                try { option.IsSetSOHold = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsEmailToSupplier.ToString()));
            if (setting != null)
            {
                try { option.IsEmailToSupplier = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsAccountActivateRequired.ToString()));
            if (setting != null)
            {
                try { option.IsAccountActivateRequired = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsShoppingSupported.ToString()));
            if (setting != null)
            {
                try { option.IsShoppingSupported = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsMultiLanguageSupported.ToString()));
            if (setting != null)
            {
                try { option.IsMultiLanguageSupported = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.DefaultLanguageId.ToString()));
            if (setting != null)
            {
                try { option.DefaultLanguageId = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.GlobalProductCatalogId.ToString()));
            if (setting != null)
            {
                try { option.GlobalProductCatalogId = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }
            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.SupplierCatalogId.ToString()));
            if (setting != null)
            {
                try { option.SupplierCatalogId = Convert.ToInt32(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.SiteCoordinatorEmail.ToString()));
            if (setting != null)
            {
                try { option.SiteCoordinatorEmail = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.QuoteCoordinatorEmail.ToString()));
            if (setting != null)
            {
                try { option.QuoteCoordinatorEmail = setting.SettingValue.Trim(); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.IsQuoteSupported.ToString()));
            if (setting != null)
            {
                try { option.IsQuoteSupported = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

            setting = settings.SingleOrDefault(o => o.SettingKey.Equals(ApplicationSettingKeys.EnableRegister.ToString()));
            if (setting != null)
            {
                try { option.EnableRegister = Convert.ToBoolean(setting.SettingValue.Trim()); }
                catch { }
            }

        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IApplicationSettingService service = UnitOfWork.GetService<IApplicationSettingService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (ApplicationSettingData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.SettingKey));
                }
            }

            return dataSource;
        }
    }
}
