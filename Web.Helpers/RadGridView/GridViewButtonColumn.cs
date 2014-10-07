using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewButtonColumn : GridViewColumn
    {
        public override GridColumn CreateColumn()
        {
            GridButtonColumn column = new GridButtonColumn();
            return column;
        }
    }
}