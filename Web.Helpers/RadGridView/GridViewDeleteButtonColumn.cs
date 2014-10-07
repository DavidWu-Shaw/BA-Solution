using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewDeleteButtonColumn : GridViewColumn
    {
        public string ConfirmText { get; set; }
        public string ConfirmTextFormatString { get; set; }
        public string[] ConfirmTextFields { get; set; }

        public override GridColumn CreateColumn()
        {
            GridButtonColumn column = new GridButtonColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);

            GridButtonColumn column = (GridButtonColumn)gridColumn;
            column.ButtonType = GridButtonColumnType.ImageButton;
            column.Text = "Delete";
            column.CommandName = RadGrid.DeleteCommandName;
            column.HeaderStyle.Width = ButtonColumnWidth;
            column.ConfirmDialogType = GridConfirmDialogType.RadWindow;
            column.ConfirmTitle = "Delete Confirm";
            column.ConfirmText = ConfirmText;
            column.ConfirmTextFormatString = ConfirmTextFormatString;
            column.ConfirmTextFields = ConfirmTextFields;
        }
    }
}