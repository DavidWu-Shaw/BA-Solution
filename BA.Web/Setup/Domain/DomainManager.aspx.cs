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
using CRM.Core;
using Framework.Component;
using Setup.Data;
using Setup.Component.Converters;

namespace BA.Web.Setup.Domain
{
    public partial class DomainManager : AdminSetupBasePage
    {
        public const string QryDomainId = "DomainId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryDomainId].TryToParse<int>();
            }
        }

        private DomainDto _currentInstance;
        private DomainDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            DomainFacade facade = new DomainFacade(uow);
                            _currentInstance = facade.RetrieveOrNewDomain(InstanceId);
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as DomainDto;
                    }
                }

                return _currentInstance;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControl();
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.CurrentInstance = CurrentInstance;
            ucIDetail.AllowModify = CurrentUserContext.IsAdmin;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.DomainMainMenu:
                    e.Instances = CurrentInstance.DomainMainMenus;
                    break;
                case InstanceTypes.DomainSetupMenu:
                    e.Instances = CurrentInstance.DomainSetupMenus;
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
            {
                case InstanceTypes.DomainMainMenu:
                    e.Instance = new DomainMainMenuDto();
                    break;
                case InstanceTypes.DomainSetupMenu:
                    e.Instance = new DomainSetupMenuDto();
                    break;
            }
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DomainFacade facade = new DomainFacade(uow);

                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.DomainMainMenu:
                        DomainMainMenuDto itemDto = e.Instance as DomainMainMenuDto;
                        // Save data
                        IFacadeUpdateResult<DomainData> result1 = facade.SaveDomainMainMenu(CurrentInstance.Id, itemDto);
                        e.IsSuccessful = result1.IsSuccessful;
                        if (result1.IsSuccessful)
                        {
                            // Refresh 
                            DomainDto savedParentInstance = result1.ToDto(new DomainConverter());
                            CurrentInstance.DomainMainMenus = savedParentInstance.DomainMainMenus;
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result1.ValidationResult, result1.Exception);
                        }
                        break;
                    case InstanceTypes.DomainSetupMenu:
                        DomainSetupMenuDto itemDto2 = e.Instance as DomainSetupMenuDto;
                        // Save data
                        IFacadeUpdateResult<DomainData> result2 = facade.SaveDomainSetupMenu(CurrentInstance.Id, itemDto2);
                        e.IsSuccessful = result2.IsSuccessful;
                        if (result2.IsSuccessful)
                        {
                            // Refresh 
                            DomainDto savedParentInstance = result2.ToDto(new DomainConverter());
                            CurrentInstance.DomainSetupMenus = savedParentInstance.DomainSetupMenus;
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result2.ValidationResult, result2.Exception);
                        }
                        break;
                }
            }
        }

        protected void ucIDetail_ChildListInstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DomainFacade facade = new DomainFacade(uow);

                switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), e.InstanceType))
                {
                    case InstanceTypes.DomainMainMenu:
                        IFacadeUpdateResult<DomainData> result = facade.DeleteDomainMainMenu(CurrentInstance.Id, e.Instance.Id);
                        e.IsSuccessful = result.IsSuccessful;

                        if (result.IsSuccessful)
                        {
                            // Refresh whole list
                            DomainDto savedParentInstance = result.ToDto(new DomainConverter());
                            CurrentInstance.DomainMainMenus = savedParentInstance.DomainMainMenus;
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result.ValidationResult, result.Exception);
                        }
                        break;
                    case InstanceTypes.DomainSetupMenu:
                        IFacadeUpdateResult<DomainData> result2 = facade.DeleteDomainSetupMenu(CurrentInstance.Id, e.Instance.Id);
                        e.IsSuccessful = result2.IsSuccessful;

                        if (result2.IsSuccessful)
                        {
                            // Refresh whole list
                            DomainDto savedParentInstance = result2.ToDto(new DomainConverter());
                            CurrentInstance.DomainSetupMenus = savedParentInstance.DomainSetupMenus;
                        }
                        else
                        {
                            // Deal with Update result
                            ProcUpdateResult(result2.ValidationResult, result2.Exception);
                        }
                        break;
                }
            }
        }

        private void SetButtonAttribute()
        {
            if (InstanceId.HasValue)
            {
                //ucIDetail.EditPostBackUrl = GetPageUrl(SDApplicationPage.ProjectEdit, new SDPageParameter(ProjectEdit.QryProjectID, ProjectId));
            }
        }
    }

}