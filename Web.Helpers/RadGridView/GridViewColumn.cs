using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public abstract class GridViewColumn
    {
        public const int ButtonColumnWidth = 60;

        public string Caption { get; set; }
        public string UniqueName { get; set; }
        public int EditColumnIndex { get; set; }
        public bool NonExportable { get; set; }

        public abstract GridColumn CreateColumn();

        public virtual void AttachProperties(GridColumn gridColumn)
        {
            gridColumn.HeaderText = Caption;
            gridColumn.EditFormColumnIndex = EditColumnIndex - 1;
            gridColumn.AutoPostBackOnFilter = true;
            gridColumn.ShowFilterIcon = false;
            if (UniqueName != null && UniqueName.Trim().Length > 0)
            {
                gridColumn.UniqueName = UniqueName.Trim().Replace(' ', '_');
            }
        }
    }
}