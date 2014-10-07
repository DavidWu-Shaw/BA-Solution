using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Telerik.Web.UI;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public class LinkButtonEditItemTemplate : IBindableTemplate
    {
        private string ControlId { get; set; }
        private string DataField { get; set; }

        public LinkButtonEditItemTemplate(string controlId, string dataField)
        {
            ControlId = controlId;
            DataField = dataField;
        }

        public void InstantiateIn(Control container)
        {
            RadTextBox textBox = new RadTextBox();
            container.Controls.Add(textBox);
            textBox.ID = ControlId;
        }

        #region IBindableTemplate Members

        public System.Collections.Specialized.IOrderedDictionary ExtractValues(Control container)
        {
            OrderedDictionary od = new OrderedDictionary();
            RadTextBox textBox = (RadTextBox)container.FindControl(ControlId);
            od.Add(DataField, textBox.Text);
            return od;
        }

        #endregion
    }
}