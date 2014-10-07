using System;
using System.Collections.Generic;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using Framework.UoW;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Data;

namespace BA.Web.Setup
{
    public partial class ApplicationSettingMgt : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/ApplicationSettingMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<ApplicationSettingDto> _currentInstances;
        public IEnumerable<ApplicationSettingDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<ApplicationSettingDto>;
                }

                return _currentInstances;
            }
            set
            {
                _currentInstances = value;
                SaveInSession(_currentInstances, UniqueInstancesStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RetrieveData();
            }

            InitInstanceList();
        }

        private void InitInstanceList()
        {
            ucIList.NeedListInstances += new NeedListInstancesEventHandler(ucIList_NeedListInstances);
            ucIList.InstanceRowSaving += new InstanceRowSavingEventHandler(ucIList_InstanceRowSaving);

            ucIList.AllowEdit = true;

            PageName = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ApplicationSettingFacade facade = new ApplicationSettingFacade(uow);
                CurrentInstances = facade.RetrieveAllApplicationSetting();
            }
        }

        protected void ucIList_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            e.Instances = CurrentInstances;
        }

        protected void ucIList_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ApplicationSettingDto project = e.Instance as ApplicationSettingDto;
                ApplicationSettingFacade facade = new ApplicationSettingFacade(uow);
                IFacadeUpdateResult<ApplicationSettingData> result = facade.SaveApplicationSetting(project);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    CurrentInstances = facade.RetrieveAllApplicationSetting();
                    // Refresh global cache 
                    //WebContext.Current.ApplicationOption = facade.GetApplicationOption();
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }
    }
}