using Telerik.Web.UI;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public class GridViewDataDateColumn : GridViewDataColumn
    {
        public string DateFormatString { get; set; }

        public GridViewDataDateColumn(string dataFieldKey)
            : base(dataFieldKey)
        {
        }

        public GridViewDataDateColumn(string caption, string dataFieldKey)
            : base(caption, dataFieldKey)
        {
        }

        public override GridColumn CreateColumn()
        {
            GridDateTimeColumn column = new GridDateTimeColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);
            GridDateTimeColumn column = (GridDateTimeColumn)gridColumn;
            column.PickerType = GridDateTimeColumnPickerType.DatePicker;
            // Disable filter
            column.AllowFiltering = false;
            if (DateFormatString != null)
            {
                column.DataFormatString = DateFormatString;
            }
            else
            {
                column.DataFormatString = UIConst.DateFormatString;
            }
        }
    }
}