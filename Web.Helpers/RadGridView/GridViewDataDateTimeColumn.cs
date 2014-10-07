using Telerik.Web.UI;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public class GridViewDataDateTimeColumn : GridViewDataColumn
    {
        public string DateTimeFormatString { get; set; }

        public GridViewDataDateTimeColumn(string dataFieldKey)
            : base(dataFieldKey)
        {
        }

        public GridViewDataDateTimeColumn(string caption, string dataFieldKey)
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
            column.PickerType = GridDateTimeColumnPickerType.DateTimePicker;
            column.AllowFiltering = false;
            if (DateTimeFormatString != null)
            {
                column.DataFormatString = DateTimeFormatString;
            }
            else
            {
                column.DataFormatString = UIConst.DateTimeFormatString;
            }
        }
    }
}