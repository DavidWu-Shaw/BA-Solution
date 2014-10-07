using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    // = GridBoundColumn
    public class GridViewDataColumn : GridViewColumn
    {
        public string DataFieldKey { get; set; }
        public bool IsReadOnly { get; set; }
        public int MaxLength { get; set; }

        public GridViewDataColumn(string dataFieldKey)
        {
            Caption = dataFieldKey;
            DataFieldKey = dataFieldKey;
            UniqueName = dataFieldKey;
        }

        public GridViewDataColumn(string caption, string dataFieldKey)
        {
            Caption = string.IsNullOrEmpty(caption) ? dataFieldKey : caption;
            DataFieldKey = dataFieldKey;
            UniqueName = dataFieldKey;
        }

        public override GridColumn CreateColumn()
        {
            GridBoundColumn column = new GridBoundColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);
            GridBoundColumn column = (GridBoundColumn)gridColumn;
            column.DataField = DataFieldKey;
            column.ReadOnly = IsReadOnly;
            column.MaxLength = MaxLength;
        }
    }
}