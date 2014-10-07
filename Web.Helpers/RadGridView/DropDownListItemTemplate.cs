using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Core;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class DropDownListItemTemplate : ITemplate
    {
        public IEnumerable<BindingListItem> ListDataSource { get; set; }
        
        private string DataField { get; set; }

        public DropDownListItemTemplate(string dataField)
        {
            DataField = dataField;
        }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            Label label = new Label();
            label.DataBinding += new EventHandler(label_DataBinding);
            container.Controls.Add(label);
        }

        #endregion

        private void label_DataBinding(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            GridItem container = (GridItem)label.NamingContainer;
            object data = DataBinder.Eval(container.DataItem, DataField);
            if (data != null && ListDataSource != null)
            {
                label.Text = GetText(data);
            }
        }

        private string GetText(object value)
        {
            BindingListItem item = ListDataSource.FirstOrDefault(o => object.Equals(o.Value, value));
            if (item != null)
            {
                return item.Text;
            }
            else
            {
                return string.Empty;
            }
        }
    }

}