using System;
using System.Collections.Generic;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;
using CRM.Core;
using BA.Web.Converters;

namespace BA.Web.WebPages.Document
{
    public partial class DocumentMgt : AppBasePage
    {
        public const string PageUrl = @"/WebPages/Document/DocumentMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<DocumentDto> _currentInstances;
        public IEnumerable<DocumentDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<DocumentDto>;
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

            if (!CurrentUserContext.IsAdmin)
            {
                ucIList.ControlledFieldName = WebContext.Current.DomainSettingList[CurrentUserContext.User.DomainId].RelatedSubjectIdField;
            }

            PageName = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.ListLabel = ucIList.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
            ucIList.AllowFilteringByColumn = ucIList.CurrentSubject.AllowListFiltering;

            bool allowModify = CurrentUserContext.IsEmployee || CurrentUserContext.IsAdmin;
            ucIList.AllowAdd = ucIList.CurrentSubject.AllowListAdd && allowModify;
            ucIList.AllowEdit = ucIList.CurrentSubject.AllowListEdit && allowModify;
            ucIList.AllowDelete = ucIList.CurrentSubject.AllowListDelete && allowModify;
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DocumentFacade facade = new DocumentFacade(uow);
                RetrieveInstances(facade);
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
                DocumentFacade facade = new DocumentFacade(uow);
                IFacadeUpdateResult<DocumentData> result = facade.DeleteDocument(e.Instance.Id);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    RetrieveInstances(facade);
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
                DocumentDto project = e.Instance as DocumentDto;
                DocumentFacade facade = new DocumentFacade(uow);
                IFacadeUpdateResult<DocumentData> result = facade.SaveDocument(project);
                e.IsSuccessful = result.IsSuccessful;

                if (result.IsSuccessful)
                {
                    // Refresh whole list
                    RetrieveInstances(facade);
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private void RetrieveInstances(DocumentFacade facade)
        {
            if (CurrentUserContext.IsAdmin)
            {
                CurrentInstances = facade.RetrieveAllDocument(new DocumentConverter());
            }
            else if (CurrentUserContext.IsEmployee)
            {
                CurrentInstances = facade.RetrieveDocumentsByUser(CurrentUserContext.User.UserId, new DocumentConverter());
            }
        }
    }
}