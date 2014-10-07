using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewEditCommandColumn : GridViewColumn
    {
        public override GridColumn CreateColumn()
        {
            GridEditCommandColumn column = new GridEditCommandColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);

            GridEditCommandColumn column = (GridEditCommandColumn)gridColumn;
            column.ButtonType = GridButtonColumnType.ImageButton;
            column.HeaderStyle.Width = ButtonColumnWidth;
        }
    }
}