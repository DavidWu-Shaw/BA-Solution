using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewDataCheckColumn : GridViewColumn
    {
        public string DataFieldKey { get; set; }
        public bool IsReadOnly { get; set; }

        public GridViewDataCheckColumn(string caption, string dataFieldKey)
        {
            Caption = caption;
            DataFieldKey = dataFieldKey;
            UniqueName = dataFieldKey;
        }

        public override GridColumn CreateColumn()
        {
            GridCheckBoxColumn column = new GridCheckBoxColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);

            GridCheckBoxColumn column = (GridCheckBoxColumn)gridColumn;
            column.DataField = DataFieldKey;
            column.ReadOnly = IsReadOnly;
        }
    }
}