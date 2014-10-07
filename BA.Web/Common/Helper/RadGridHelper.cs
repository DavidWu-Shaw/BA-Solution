using System.Collections.Generic;
using BA.Core;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class RadGridHelper
    {
        private const int ButtonColumnWidth = 60;

        private RadGrid _grid;

        public RadGridHelper(RadGrid grid)
        {
            _grid = grid;
        }

        public static void SetStyle(RadGrid grid, int? pageSize)
        {
            grid.MasterTableView.GridLines = System.Web.UI.WebControls.GridLines.Both;
            grid.Skin = "Windows7";
            grid.CellSpacing = 0;
            grid.AutoGenerateColumns = false;
            grid.ClientSettings.EnableRowHoverStyle = true;
            grid.ClientSettings.Selecting.AllowRowSelect = true;
            grid.MasterTableView.EditMode = GridEditMode.InPlace;
            grid.AllowSorting = true;
            if (pageSize.HasValue)
            {
                grid.AllowPaging = true;
                grid.PageSize = pageSize.Value;
            }
            else
            {
                grid.AllowPaging = false;
            }
            grid.PagerStyle.Mode = GridPagerMode.NumericPages;
            grid.MasterTableView.EnableNoRecordsTemplate = true;
            grid.GroupingSettings.CaseSensitive = false;

            //grid.AlternatingItemStyle.BackColor = Color.WhiteSmoke;
            //grid.ItemStyle.BackColor = Color.WhiteSmoke;
            //grid.AlternatingItemStyle.ForeColor = _grid.ItemStyle.ForeColor;
        }

        public static void SetStyle(RadGrid grid)
        {
            SetStyle(grid, null);
        }

        public void SetStyleDefault()
        {
            SetStyle(_grid);
        }

        public GridEditCommandColumn AddEditCommandColumn()
        {
            return AddEditCommandColumn("Edit");
        }

        public GridEditCommandColumn AddEditCommandColumn(string editText)
        {
            GridEditCommandColumn ec = new GridEditCommandColumn();
            _grid.MasterTableView.Columns.Add(ec);
            ec.ButtonType = GridButtonColumnType.ImageButton;
            ec.EditText = editText;
            ec.HeaderStyle.Width = ButtonColumnWidth;
            return ec;
        }

        public GridButtonColumn AddDeleteButtonColumn()
        {
            GridButtonColumn bc = new GridButtonColumn();
            _grid.MasterTableView.Columns.Add(bc);
            bc.ButtonType = GridButtonColumnType.ImageButton;
            bc.Text = "Delete";
            bc.CommandName = "Delete";
            bc.HeaderStyle.Width = ButtonColumnWidth;
            bc.ConfirmDialogType = GridConfirmDialogType.RadWindow;
            bc.ConfirmTitle = "Delete Confirm";
            return bc;
        }

        public GridButtonColumn AddButtonColumn(string buttonText, string commandName)
        {
            GridButtonColumn bc = new GridButtonColumn();
            _grid.MasterTableView.Columns.Add(bc);
            bc.ButtonType = GridButtonColumnType.LinkButton;
            bc.ConfirmDialogType = GridConfirmDialogType.Classic;
            bc.Text = buttonText;
            bc.CommandName = commandName;
            bc.HeaderStyle.Width = ButtonColumnWidth;
            return bc;
        }

        public GridBoundColumn AddBoundColumn(string dataField, string headerText)
        {
            return AddBoundColumn(dataField, headerText, false);
        }

        public GridBoundColumn AddBoundColumn(string dataField, string headerText, bool isReadOnly)
        {
            GridBoundColumn c = new GridBoundColumn();
            _grid.MasterTableView.Columns.Add(c);
            SetBoundColumn(c, dataField, headerText, isReadOnly);
            return c;
        }

        public GridNumericColumn AddNumericColumn(string dataField, string headerText)
        {
            GridNumericColumn c = new GridNumericColumn();
            _grid.MasterTableView.Columns.Add(c);
            SetBoundColumn(c, dataField, headerText, false);
            c.NumericType = NumericType.Number;
            c.DataType = typeof(int);
            c.DecimalDigits = 0;
            c.DataFormatString = "{0:N0}";
            return c;
        }

        public GridTemplateColumn AddDropDownTemplateColumn(string dataField, string headerText, IEnumerable<BindingListItem> listDataSource)
        {
            GridTemplateColumn c = new GridTemplateColumn();
            _grid.MasterTableView.Columns.Add(c);
            c.UniqueName = dataField;
            c.DataField = dataField;
            c.HeaderText = headerText;
            DropDownListItemTemplate template = new DropDownListItemTemplate(dataField);
            c.ItemTemplate = template;
            template.ListDataSource = listDataSource;

            DropDownListEditItemTemplate editTemplate = new DropDownListEditItemTemplate(dataField, dataField);
            c.EditItemTemplate = editTemplate;
            return c;
        }

        public GridDropDownColumn AddDropDownColumn(string dataField, string headerText, bool enableEmptyItem)
        {
            GridDropDownColumn c = new GridDropDownColumn();
            _grid.MasterTableView.Columns.Add(c);
            c.DropDownControlType = GridDropDownColumnControlType.DropDownList;
            c.EnableEmptyListItem = enableEmptyItem;
            c.EmptyListItemText = BindingListItem.EmptyText;
            c.EmptyListItemValue = BindingListItem.EmptyValue;
            c.ListValueField = BindingListItem.ValueProperty;
            c.ListTextField = BindingListItem.TextProperty;
            c.HeaderText = headerText;
            c.DataField = dataField;
            return c;
        }

        public GridHyperLinkColumn AddHyperLinkColumn(string dataTextField, string headerText)
        {
            GridHyperLinkColumn c = new GridHyperLinkColumn();
            _grid.MasterTableView.Columns.Add(c);
            c.DataTextField = dataTextField;
            c.HeaderText = headerText;
            return c;
        }

        public GridCheckBoxColumn AddCheckBoxColumn(string dataField, string headerText)
        {
            GridCheckBoxColumn c = new GridCheckBoxColumn();
            _grid.MasterTableView.Columns.Add(c);
            c.DataField = dataField;
            c.HeaderText = headerText;
            return c;
        }

        public GridDateTimeColumn AddDateColumn(string dataField, string headerText, bool isReadOnly)
        {
            GridDateTimeColumn c = new GridDateTimeColumn();
            _grid.MasterTableView.Columns.Add(c);
            c.DataFormatString = UIConst.DateFormatString;
            c.PickerType = GridDateTimeColumnPickerType.DatePicker;
            //c.EditDataFormatString = CommonConst.DateFormatString;
            SetBoundColumn(c, dataField, headerText, isReadOnly);
            return c;
        }

        public GridDateTimeColumn AddDateTimeColumn(string dataField, string headerText, bool isReadOnly)
        {
            GridDateTimeColumn c = new GridDateTimeColumn();
            _grid.MasterTableView.Columns.Add(c);
            c.PickerType = GridDateTimeColumnPickerType.DateTimePicker;
            //c.DataFormatString = CommonConst.DateTimeFormatString;
            //c.EditDataFormatString = CommonConst.DateTimeFormatString;
            SetBoundColumn(c, dataField, headerText, isReadOnly);
            return c;
        }

        public static void SetBoundColumn(GridBoundColumn c, string dataField, string headerText, bool isReadOnly)
        {
            c.DataField = dataField;
            c.HeaderText = headerText;
            c.ReadOnly = isReadOnly;
        }
    }
}