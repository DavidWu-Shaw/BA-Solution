using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BA.Web.Common;
using Setup.Component.Dto;
using Framework.UoW;
using BA.UnityRegistry;
using Setup.Component;
using BA.Web.Common.Helper;
using Framework.Component;
using Setup.Data;

namespace BA.Web.Setup.Domain
{    
    public partial class DomainMgt : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/Domain/DomainMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<DomainDto> _currentInstances;
        public IEnumerable<DomainDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<DomainDto>;
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

            PageName = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.ListLabel = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.AllowFilteringByColumn = ucIList.CurrentSubject.AllowListFiltering;

            bool allowModify = CurrentUserContext.IsSuperAdmin;
            ucIList.AllowAdd = ucIList.CurrentSubject.AllowListAdd && allowModify;
            ucIList.AllowEdit = ucIList.CurrentSubject.AllowListEdit && allowModify;
            ucIList.AllowDelete = ucIList.CurrentSubject.AllowListDelete && allowModify;
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DomainFacade facade = new DomainFacade(uow);
                CurrentInstances = facade.RetrieveAllDomain();
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
                DomainDto instance = e.Instance as DomainDto;
                DomainFacade facade = new DomainFacade(uow);
                IFacadeUpdateResult<DomainData> result = facade.SaveDomain(instance);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    CurrentInstances = facade.RetrieveAllDomain();
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