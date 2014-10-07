using System.Collections.Generic;
using System.Web.UI;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class DropDownListFilterTemplate : ITemplate
    {
        public IEnumerable<BindingListItem> ListDataSource { get; set; }
        public int DropDownHeight { get; set; }

        private string DataField { get; set; }

        public DropDownListFilterTemplate(string dataField)
        {
            DataField = dataField;
        }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            RadComboBox ddl = new RadComboBox();
            container.Controls.Add(ddl);
            ddl.MarkFirstMatch = true;
            if (DropDownHeight > 0)
            {
                ddl.Height = DropDownHeight;
            }
            else
            {
                ddl.Height = 200;
            }
            ddl.Items.Add(new RadComboBoxItem(BindingListItem.EmptyText, BindingListItem.EmptyValue));
            ddl.DataTextField = BindingListItem.TextProperty;
            ddl.DataValueField = BindingListItem.ValueProperty;
            ddl.DataSource = ListDataSource;
            ddl.AppendDataBoundItems = true;
            ddl.AutoPostBack = true;
            ddl.SelectedIndexChanged += new RadComboBoxSelectedIndexChangedEventHandler(ddl_SelectedIndexChanged);
            ddl.SelectedValue = ((GridItem)container.Parent).OwnerTableView.GetColumn(DataField).CurrentFilterValue;
        }

        private void ddl_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            ((GridItem)(((RadComboBox)sender).Parent.Parent)).OwnerTableView.GetColumn(DataField).CurrentFilterValue = e.Value;
            ((GridFilteringItem)(((RadComboBox)sender).Parent.Parent)).FireCommandEvent(RadGrid.FilterCommandName, new Pair(GridKnownFunction.EqualTo.ToString(), DataField));
        }

        #endregion
    }
}