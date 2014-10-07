using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using Framework.UoW;
using Setup.Component;
using Setup.Component.Dto;

namespace BA.Web.Setup.User
{
    public partial class UserManager : AdminSetupBasePage
    {
        public const string QryUserId = "UserId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryUserId].TryToParse<int>();
            }
        }

        private UserDto _currentInstance;
        private UserDto CurrentInstance
        {
            get
            {
                if (_currentInstance == null)
                {
                    if (!IsPostBack)
                    {
                        using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                        {
                            UserFacade facade = new UserFacade(uow);
                            _currentInstance = facade.RetrieveOrNewUser(InstanceId);
                        }
                        SaveInSession(_currentInstance, InstanceStateKey);
                    }
                    else if (IsInSession(InstanceStateKey))
                    {
                        _currentInstance = GetFromSession(InstanceStateKey) as UserDto;
                    }
                }

                return _currentInstance;
            }
            set
            {
                _currentInstance = value;
                SaveInSession(_currentInstance, InstanceStateKey);
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
            
            ucIDetail.AllowModify = CurrentUserContext.IsSuperAdmin;
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