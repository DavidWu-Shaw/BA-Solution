using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common.Helper;
using BA.Web.Common.UserControls;
using Framework.UoW;
using SubjectEngine.Component;
using SubjectEngine.Component.Dto;
using SubjectEngine.Data;

namespace BA.Web.Common.Subject
{
    public partial class EntityManager : AdminSetupBasePage
    {
        public const string PageUrl = "/Common/Subject/EntityManager.aspx";
        public const string QryEntityId = "EntityId";

        private string UniqueInstanceStateKey { get { return string.Format("{0}_{1}", UniqueID, InstanceStateKey); } }

        private int? EntityId
        {
            get
            {
                return Request.QueryString[QryEntityId].TryToParse<int>();
            }
        }
        private enum ChildListType
        {
            EntityItem
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

            btnEdit.PostBackUrl = string.Format("{0}?{1}={2}", ServerPath + EntityEdit.PageUrl, EntityEdit.QryEntityId, EntityId);

            InitDEntityItemList();
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade facade = new DEntityFacade(uow);
                CurrentDEntity = facade.RetrieveDEntity(EntityId);
            }
        }

        private void LoadInstanceData()
        {
            lblTitle.Text = string.Format("DEntity: {0}", CurrentDEntity.Code);

            lblCode.Text = CurrentDEntity.Code;
            lblDescription.Text = CurrentDEntity.Description;
        }

        private void InitDEntityItemList()
        {
            // DEntityField
            ListManager childListControl = (ListManager)Page.LoadControl(ServerPath + ListManager.ControlURL);
            childListControl.ID = ChildListType.EntityItem.ToString();
            childListControl.InstanceType = ChildListType.EntityItem.ToString();
            childListControl.IsChildList = true;
            childListControl.ListLabel = "Entity Item List";
            childListControl.AllowAdd = CurrentDEntity.AllowAddItem;
            childListControl.IsAddInGrid = true;
            childListControl.NeedListInstances += new NeedListInstancesEventHandler(childListControl_NeedListInstances);
            childListControl.InstanceRowNewing += new InstanceRowNewingEventHandler(childListControl_InstanceRowNewing);
            childListControl.InstanceRowSaving += new InstanceRowSavingEventHandler(childList_InstanceRowSaving);
            childListControl.InstanceRowDeleting += new InstanceRowDeletingEventHandler(childListControl_InstanceRowDeleting);

            if (CurrentDEntity.AllowEditItem)
            {
                childListControl.Columns.Add(new GridViewEditCommandColumn());
            }
            if (CurrentDEntity.AllowDeleteItem)
            {
                GridViewDeleteButtonColumn deleteColumn = new GridViewDeleteButtonColumn();
                deleteColumn.ConfirmTextFormatString = "Are you sure to delete {0}";
                deleteColumn.ConfirmTextFields = new string[] { DEntityItemDto.FLD_Text };
                childListControl.Columns.Add(deleteColumn);
            }

            childListControl.Columns.Add(new GridViewDataTextColumn("Text", DEntityItemDto.FLD_Text));
            childListControl.Columns.Add(new GridViewDataColumn("Value", DEntityItemDto.FLD_Value));

            // Add empty row above the list control
            HtmlTable table = new HtmlTable();
            HtmlTableRow row = new HtmlTableRow();
            row.Height = "10";
            table.Rows.Add(row);
            PlaceHolder1.Controls.Add(table);
            // Add the child list control
            PlaceHolder1.Controls.Add(childListControl);
        }

        protected void childListControl_InstanceRowNewing(object sender, InstanceRowNewingEventArgs e)
        {
            if (e.InstanceType == ChildListType.EntityItem.ToString())
            {
                e.Instance = new DEntityItemDto();
            }
        }

        protected void childListControl_NeedListInstances(object sender, NeedListInstancesEventArgs e)
        {
            if (e.InstanceType == ChildListType.EntityItem.ToString())
            {
                e.Instances = CurrentDEntity.DEntityItems;
            }
        }

        protected void childList_InstanceRowSaving(object sender, InstanceRowSavingEventArgs e)
        {
            if (e.InstanceType == ChildListType.EntityItem.ToString())
            {
                DEntityItemDto item = e.Instance as DEntityItemDto;
                IFacadeUpdateResult<DEntityData> result = SaveDEntityItem(CurrentDEntity.Id, item);
                e.IsSuccessful = result.IsSuccessful;
                if (result.IsSuccessful)
                {
                    // Refresh data in session
                    RetrieveData();
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected void childListControl_InstanceRowDeleting(object sender, InstanceRowDeletingEventArgs e)
        {
            if (e.InstanceType == ChildListType.EntityItem.ToString())
            {
                IFacadeUpdateResult<DEntityData> result = DeleteDEntityItem(CurrentDEntity.Id, e.Instance.Id);
                e.IsSuccessful = result.IsSuccessful;
                if (result.IsSuccessful)
                {
                    // Refresh data in session
                    RetrieveData();
                }
                else
                {
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private IFacadeUpdateResult<DEntityData> SaveDEntityItem(object entityId, DEntityItemDto item)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade facade = new DEntityFacade(uow);
                IFacadeUpdateResult<DEntityData> result = facade.SaveDEntityItem(entityId, item);
                return result;
            }
        }

        private IFacadeUpdateResult<DEntityData> DeleteDEntityItem(object entityId, object itemId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DEntityFacade facade = new DEntityFacade(uow);
                IFacadeUpdateResult<DEntityData> result = facade.DeleteDEntityItem(entityId, itemId);
                return result;
            }
        }
    }
}