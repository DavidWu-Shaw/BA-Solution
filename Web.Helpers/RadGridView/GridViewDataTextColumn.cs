
using Telerik.Web.UI;
namespace BA.Web.Common.Helper
{
    public class GridViewDataTextColumn : GridViewDataColumn
    {
        public GridViewDataTextColumn(string dataFieldKey)
            : base(dataFieldKey)
        {
        }

        public GridViewDataTextColumn(string caption, string dataFieldKey)
            : base(caption, dataFieldKey)
        {
        }

        public override void AttachProperties(Telerik.Web.UI.GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);

            GridBoundColumn column = (GridBoundColumn)gridColumn;
            column.CurrentFilterFunction = GridKnownFunction.Contains;
        }
    }
}