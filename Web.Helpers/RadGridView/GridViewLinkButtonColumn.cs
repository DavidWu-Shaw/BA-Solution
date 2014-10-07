using System.Collections.Generic;
using Telerik.Web.UI;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public class GridViewLinkButtonColumn : GridViewColumn
    {
        public string DataNavigateUrlField { get; set; }
        public string DataNavigateUrlFormatString { get; set; }
        public string DataTextField { get; set; }
        public bool IsReadOnly { get; set; }

        public GridViewLinkButtonColumn(string headerText, string dataFieldKey)
        {
            Caption = headerText;
            DataTextField = dataFieldKey;
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
            column.HeaderText = Caption;
            column.DataField = DataTextField;
            column.CurrentFilterFunction = GridKnownFunction.EqualTo;

            LinkButtonItemTemplate template = new LinkButtonItemTemplate(DataTextField);
            column.ItemTemplate = template;
            template.DataNavigateUrlField = DataNavigateUrlField;
            template.DataNavigateUrlFormatString = DataNavigateUrlFormatString;

            if (!IsReadOnly)
            {
                LinkButtonEditItemTemplate editTemplate = new LinkButtonEditItemTemplate(DataTextField, DataTextField);
                column.EditItemTemplate = editTemplate;
            }
        }
    }
}