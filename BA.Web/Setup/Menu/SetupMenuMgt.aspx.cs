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

namespace BA.Web.Setup.Menu
{
    public partial class SetupMenuMgt : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/Menu/SetupMenuMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<SetupMenuDto> _currentInstances;
        public IEnumerable<SetupMenuDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<SetupMenuDto>;
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
            ucIList.InstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIList_InstanceRowDeleting);
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
                SetupMenuFacade facade = new SetupMenuFacade(uow);
                CurrentInstances = facade.RetrieveAllSetupMenu();
            }
        }

        protected void ucIList_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            e.Instances = CurrentInstances;
        }

        protected void ucIList_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SetupMenuFacade facade = new SetupMenuFacade(uow);
                IFacadeUpdateResult<SetupMenuData> result = facade.DeleteSetupMenu(e.Instance.Id);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    CurrentInstances = facade.RetrieveAllSetupMenu();
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected void ucIList_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SetupMenuDto project = e.Instance as SetupMenuDto;
                SetupMenuFacade facade = new SetupMenuFacade(uow);
                IFacadeUpdateResult<SetupMenuData> result = facade.SaveSetupMenu(project);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    CurrentInstances = facade.RetrieveAllSetupMenu();
                    // Refresh global cache 
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