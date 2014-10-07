using System.Collections.Generic;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewDropDownListColumn : GridViewColumn
    {
        public string DataFieldKey { get; set; }
        public bool IsReadOnly { get; set; }
        public IEnumerable<BindingListItem> ListDataSource { get; set; }
        public int DropDownHeight { get; set; }
        public bool enableEmptyItem { get; set; }

        public GridViewDropDownListColumn(string headerText, string dataFieldKey, IEnumerable<BindingListItem> listDataSource)
        {
            Caption = headerText;
            DataFieldKey = dataFieldKey;
            UniqueName = dataFieldKey;
            ListDataSource = listDataSource;
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
            column.DataField = DataFieldKey;
            column.CurrentFilterFunction = GridKnownFunction.EqualTo;

            DropDownListItemTemplate template = new DropDownListItemTemplate(DataFieldKey);
            column.ItemTemplate = template;
            template.ListDataSource = ListDataSource;

            if (!IsReadOnly)
            {
                DropDownListEditItemTemplate editTemplate = new DropDownListEditItemTemplate(DataFieldKey, DataFieldKey);
                column.EditItemTemplate = editTemplate;
                editTemplate.DropDownHeight = DropDownHeight;
            }

            DropDownListFilterTemplate filterTemplate = new DropDownListFilterTemplate(DataFieldKey);
            column.FilterTemplate = filterTemplate;
            filterTemplate.ListDataSource = ListDataSource;
            filterTemplate.DropDownHeight = DropDownHeight;
        }
    }
}