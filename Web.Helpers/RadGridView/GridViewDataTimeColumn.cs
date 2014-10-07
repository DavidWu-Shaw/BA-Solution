using Telerik.Web.UI;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public class GridViewDataTimeColumn : GridViewDataColumn
    {
        public string TimeFormatString { get; set; }

        public GridViewDataTimeColumn(string dataFieldKey)
            : base(dataFieldKey)
        {
        }

        public GridViewDataTimeColumn(string caption, string dataFieldKey)
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
            column.PickerType = GridDateTimeColumnPickerType.TimePicker;
            column.AllowFiltering = false;
            if (TimeFormatString != null)
            {
                column.DataFormatString = TimeFormatString;
            }
            else
            {
                column.DataFormatString = UIConst.TimeFormatString;
            }
        }
    }
}