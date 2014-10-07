using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewHyperLinkColumn : GridViewColumn
    {
        public string[] DataNavigateUrlFields { get; set; }
        public string DataNavigateUrlFormatString { get; set; }
        public string DataTextField { get; set; }
        public string DataTextFormatString { get; set; }
        public string Text { get; set; }

        public override GridColumn CreateColumn()
        {
            GridHyperLinkColumn column = new GridHyperLinkColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);

            GridHyperLinkColumn column = (GridHyperLinkColumn)gridColumn;
            column.DataNavigateUrlFields = DataNavigateUrlFields;
            column.DataNavigateUrlFormatString = DataNavigateUrlFormatString;
            column.DataTextField = DataTextField;
            column.Text = Text;
        }
    }
}