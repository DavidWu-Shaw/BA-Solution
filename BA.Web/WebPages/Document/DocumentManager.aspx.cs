using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;
using BA.Web.Converters;

namespace BA.Web.WebPages.Document
{
    public partial class DocumentManager : AppBasePage
    {
        public const string QryDocumentId = "DocumentId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryDocumentId].TryToParse<int>();
            }
        }

        private DocumentDto _currentInstance;
        private DocumentDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            DocumentFacade facade = new DocumentFacade(uow);
                            _currentInstance = facade.RetrieveOrNewDocument(InstanceId, new DocumentConverter());
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as DocumentDto;
                    }
                }

                return _currentInstance;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControl();
        }

        protected override bool CheckPagePermission()
        {
            if (CurrentUserContext.IsAdmin || (CurrentUserContext.IsEmployee && object.Equals(CurrentInstance.IssuedById, CurrentUserContext.User.UserId)))
            {
                return true;
            }
            return false;
        }

        private void InitControl()
        {
            ucIDetail.ChildListNeedInstances += new NeedListInstancesEventHandler(ucIDetail_ChildListNeedInstances);
            ucIDetail.ChildListInstanceRowDeleting += new InstanceRowDeletingEventHandler(ucIDetail_ChildListInstanceRowDeleting);
            ucIDetail.ChildListInstanceRowNewing += new InstanceRowNewingEventHandler(ucIDetail_ChildListInstanceRowNewing);
            ucIDetail.ChildListInstanceRowSaving += new InstanceRowSavingEventHandler(ucIDetail_ChildListInstanceRowSaving);

            ucIDetail.AllowModify = CurrentUserContext.IsEmployee || CurrentUserContext.IsAdmin;
            ucIDetail.CurrentInstance = CurrentInstance;
            PageName = ucIDetail.CurrentSubject.GetSubjectLabelByLanguage(CurrentUserContext.CurrentLanguage.Id);
        }

        protected void ucIDetail_ChildListNeedInstances(object sender, NeedListInstancesEventArgs e)
        {
        }

        protected void ucIDetail_ChildListInstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
        }

        protected void ucIDetail_ChildListInstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
        }

        protected void ucIDetail_ChildListInstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
        }
    }
}