using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewAttachmentColumn : GridViewColumn
    {
        public string DataFieldKey { get; set; }
        public bool IsReadOnly { get; set; }
        public string NavigateUrlField { get; set; }
        public string NavigateUrlFormatString { get; set; }
        public string LinkTextField { get; set; }

        public GridViewAttachmentColumn(string caption, string dataFieldKey)
        {
            Caption = string.IsNullOrEmpty(caption) ? dataFieldKey : caption;
            DataFieldKey = dataFieldKey;
            UniqueName = dataFieldKey;
        }

        public override GridColumn CreateColumn()
        {
            GridTemplateColumn column = new GridTemplateColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);

            GridTemplateColumn column = (GridTemplateColumn)gridColumn;

            HyperLinkItemTemplate template = new HyperLinkItemTemplate();
            column.ItemTemplate = template;
            template.LinkTextField = LinkTextField;
            template.NavigateUrlField = NavigateUrlField;
            template.NavigateUrlFormatString = NavigateUrlFormatString;

            //if (!IsReadOnly)
            //{
            //    AttachmentEditItemTemplate editTemplate = new AttachmentEditItemTemplate(DataFieldKey, DataFieldKey);
            //    column.EditItemTemplate = editTemplate;
            //}
        }
    }
}