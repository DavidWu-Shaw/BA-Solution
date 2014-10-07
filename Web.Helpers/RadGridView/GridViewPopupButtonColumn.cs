using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewPopupButtonColumn : GridViewColumn
    {
        public string DataTextField { get; set; }
        public string PopupUrlField { get; set; }
        public string PopupUrlFormatString { get; set; }

        public override GridColumn CreateColumn()
        {
            GridTemplateColumn column = new GridTemplateColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);

            GridTemplateColumn column = (GridTemplateColumn)gridColumn;
            PopupButtonItemTemplate template = new PopupButtonItemTemplate(DataTextField);
            template.PopupUrlField = PopupUrlField;
            template.PopupUrlFormatString = PopupUrlFormatString;
            column.ItemTemplate = template;
        }
    }
}