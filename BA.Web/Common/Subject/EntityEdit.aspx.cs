using System;
using BA.Web.Common.Helper;
using Framework.UoW;
using BA.UnityRegistry;
using SubjectEngine.Component;
using SubjectEngine.Component.Dto;
using Framework.Component;
using SubjectEngine.Data;
using SubjectEngine.Component.Converters;

namespace BA.Web.Common.Subject
{
    public partial class EntityEdit : AdminSetupBasePage
    {
        public const string PageUrl = "/Common/Subject/EntityEdit.aspx";
        public const string QryEntityId = "EntityId";

        private string UniqueInstanceStateKey { get { return string.Format("{0}_{1}", UniqueID, InstanceStateKey); } }

        private int? EntityId
        {
            get
            {
                return Request.QueryString[QryEntityId].TryToParse<int>();
            }
        }

        private DEntityDto _currentDEntity;
        public DEntityDto CurrentDEntity
        {
            get
            {
                if (_currentDEntity == null && IsPostBack && IsInSession(UniqueInstanceStateKey))
                {
                    _currentDEntity = GetFromSession(UniqueInstanceStateKey) as DEntityDto;
                }

                return _currentDEntity;
            }
            set
            {
                _currentDEntity = value;
                SaveInSession(_currentDEntity, UniqueInstanceStateKey);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Entity";

            if (!IsPostBack)
            {
                RetrieveData();
                LoadInstanceData();
            }

            SetButtonAttributes();
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade facade = new DEntityFacade(uow);
                CurrentDEntity = facade.RetrieveOrNewDEntity(EntityId);
            }
        }

        private void LoadInstanceData()
        {
            lblTitle.Text = string.Format("DEntity: {0}", CurrentDEntity.Code);

            txtCode.Text = CurrentDEntity.Code;
            txtDescription.Text = CurrentDEntity.Description;
        }

        private void SetButtonAttributes()
        {
            if (CurrentDEntity.IsNew)
            {
                btnCancel.PostBackUrl = GetUrl(EntityMgt.PageUrl);
            }
            else
            {
                btnCancel.PostBackUrl = string.Format("{0}?{1}={2}", ServerPath + EntityManager.PageUrl, EntityManager.QryEntityId, EntityId);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string display = string.Empty;

            bool isValid = ValidateValue();

            if (isValid)
            {
                CurrentDEntity.Code = txtCode.Text.Trim();
                CurrentDEntity.Description = txtDescription.Text.Trim();

                bool isSuccessful = SaveInstance();

                if (isSuccessful)
                {
                    display = "* Save OK.";
                }
                else
                {
                    display = "* Save failed.";
                }
            }
            else
            {
                display = "* Invalid input.";
            }

            lbResultMsg.Text = display;
        }

        private bool ValidateValue()
        {
            return txtCode.Text.Trim().Length > 0;
        }

        protected bool SaveInstance()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade facade = new DEntityFacade(uow);
                IFacadeUpdateResult<DEntityData> result = facade.SaveDEntity(CurrentDEntity);
                if (result.IsSuccessful)
                {
                    // Refresh Instance
                    CurrentDEntity = result.ToDto<DEntityDto>(new DEntityConverter());
                    return true;
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                    return false;
                }
            }
        }
    }
}