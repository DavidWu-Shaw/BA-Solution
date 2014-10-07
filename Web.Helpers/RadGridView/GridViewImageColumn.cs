using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewImageColumn : GridViewColumn
    {
        public override GridColumn CreateColumn()
        {
            GridImageColumn column = new GridImageColumn();
            return column;
        }
    }
}