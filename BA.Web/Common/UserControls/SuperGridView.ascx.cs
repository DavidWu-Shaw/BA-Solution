using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BA.Web.Common.Helper;
using Telerik.Web.UI;
using System.Data;

namespace BA.Web.Common.UserControls
{
    public partial class SuperGridView : BaseControl
    {
        private const string DataSourceStateKey = "SuperGridView_DataSourceStateKey";

        public bool IsAutoGenerateColumns { get; set; }
        public int? PageSize { get; set; }
        public bool AllowScroll { get; set; }
        public List<GridViewColumn> Columns { get; private set; }
        public string KeyFieldName { get; set; }

        public string ListLabel
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public object DataSource
        {
            get
            {
                if (IsInSession(UniqueID + DataSourceStateKey))
                {
                    return GetFromSession(UniqueID + DataSourceStateKey);
                }
                else
                {
                    return null;
                }
            }
            set
            {
                SaveInSession(value, UniqueID + DataSourceStateKey);
                // reset page index
                gvList.CurrentPageIndex = 0;
            }
        }

        public SuperGridView()
        {
            KeyFieldName = string.Empty;
            Columns = new List<GridViewColumn>();
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            InitDataSource();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitGridView();

            if (!IsPostBack)
            {
                if (!IsAutoGenerateColumns)
                {
                    CreateColumns();
                }
            }

            GridDataBind();
        }

        private void InitDataSource()
        {
            if (!IsPostBack)
            {
                DataSource = new DataTable();
            }
        }

        private void InitGridView()
        {
            RadGridHelper.SetStyle(gvList, PageSize);
            gvList.AutoGenerateColumns = IsAutoGenerateColumns;
            gvList.ClientSettings.Scrolling.AllowScroll = AllowScroll;
        }

        private void CreateColumns()
        {
            gvList.MasterTableView.DataKeyNames = new string[] { KeyFieldName };
            gvList.MasterTableView.Columns.Clear();
            foreach (GridViewColumn column in Columns)
            {
                GridColumn radColumn = column.CreateColumn();
                gvList.MasterTableView.Columns.Add(radColumn);
                column.AttachProperties(radColumn);
            }
        }

        public void GridDataBind()
        {
            gvList.DataSource = DataSource;
            gvList.DataBind();
        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            gvList.ExportSettings.ExportOnlyData = true;
            gvList.ExportSettings.OpenInNewWindow = true;
            gvList.ExportSettings.IgnorePaging = true;
            if (ListLabel.TrimHasValue())
            {
                gvList.ExportSettings.FileName = ListLabel;
            }
            else
            {
                gvList.ExportSettings.FileName = "List";
            }

            gvList.MasterTableView.ExportToExcel();
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();

            if (!ListLabel.TrimHasValue())
            {
                lblTitle.Text = Localize("Common.UserControls.SuperGridView.lblTitle", "Data Preview: ");
            }
            lblExport.Text = Localize("Common.UserControls.SuperGridView.lblExport", "Export");
        }
    }
}