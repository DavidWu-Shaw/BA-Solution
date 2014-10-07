using System;
using System.Collections.Generic;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common.Helper;
using Framework.UoW;
using SubjectEngine.Component;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace BA.Web.Common.Subject
{
    public partial class EntityMgt : AdminSetupBasePage
    {
        public const string PageUrl = "/Common/Subject/EntityMgt.aspx";

        private const string InstancesStateKey = "InstancesSessionKey";
        private string UniqueInstancesStateKey { get { return string.Format("{0}_{1}", UniqueID, InstancesStateKey); } }

        private IEnumerable<DEntityDto> _currentInstances;
        public IEnumerable<DEntityDto> CurrentInstances
        {
            get
            {
                if (_currentInstances == null && IsInSession(UniqueInstancesStateKey))
                {
                    _currentInstances = GetFromSession(UniqueInstancesStateKey) as IEnumerable<DEntityDto>;
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
            PageName = "Entity";

            InitListManager();

            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void InitListManager()
        {
            ucListManager.ID = "DEntityList";
            ucListManager.IsChildList = false;
            ucListManager.ListLabel = "Entity List";
            ucListManager.AllowAdd = true;
            ucListManager.AddBtnPostBackUrl = GetUrl(EntityEdit.PageUrl);

            ucListManager.NeedListInstances += new NeedListInstancesEventHandler(ucListManager_NeedListInstances);
            ucListManager.InstanceRowNewing += new InstanceRowNewingEventHandler(ucListManager_InstanceRowNewing);
            ucListManager.InstanceRowSaving += new InstanceRowSavingEventHandler(ucListManager_InstanceRowSaving);
            
            ucListManager.Columns.Add(new GridViewEditCommandColumn());

            GridViewHyperLinkColumn hc = new GridViewHyperLinkColumn();
            ucListManager.Columns.Add(hc);
            hc.Caption = "Entity Code";
            hc.DataTextField = DEntityDto.FLD_Code;
            hc.DataNavigateUrlFormatString = string.Format("{0}?{1}={{0}}", ServerPath + EntityManager.PageUrl, EntityManager.QryEntityId);
            hc.DataNavigateUrlFields = new string[] { DEntityDto.FLD_StringId };

            ucListManager.Columns.Add(new GridViewDataTextColumn("Description", DEntityDto.FLD_Description));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Allow Add Item", DEntityDto.FLD_AllowAddItem));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Allow Edit Item", DEntityDto.FLD_AllowEditItem));
            ucListManager.Columns.Add(new GridViewDataCheckColumn("Allow Delete Item", DEntityDto.FLD_AllowDeleteItem));
            GridViewDataCheckColumn cc = new GridViewDataCheckColumn("Is BuiltIn", DEntityDto.FLD_IsBuiltIn);
            ucListManager.Columns.Add(cc);
            cc.IsReadOnly = true;
        }

        protected void ucListManager_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            DEntityDto instance = e.Instance as DEntityDto;
            IFacadeUpdateResult<DEntityData> result = SaveDEntity(instance);
            e.IsSuccessful = result.IsSuccessful;
        }

        protected void ucListManager_InstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            e.Instance = new DEntityDto();
        }

        protected void ucListManager_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            e.Instances = CurrentInstances;
        }

        private void LoadData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade facade = new DEntityFacade(uow);
                CurrentInstances = facade.RetrieveAllDEntity();
            }
        }

        private IFacadeUpdateResult<DEntityData> SaveDEntity(DEntityDto instance)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade facade = new DEntityFacade(uow);
                IFacadeUpdateResult<DEntityData> result = facade.SaveDEntity(instance);
                if (result.IsSuccessful)
                {
                    CurrentInstances = facade.RetrieveAllDEntity();
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }

                return result;
            }
        }
    }
}