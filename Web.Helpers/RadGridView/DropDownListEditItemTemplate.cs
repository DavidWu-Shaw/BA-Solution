using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Telerik.Web.UI;
using BA.Core;

namespace BA.Web.Common.Helper
{
    public class DropDownListEditItemTemplate : IBindableTemplate
    {
        public int DropDownHeight { get; set; }
        private string ControlId { get; set; }
        private string DataField { get; set; }

        public DropDownListEditItemTemplate(string controlId, string dataField)
        {
            ControlId = controlId;
            DataField = dataField;
        }

        public void InstantiateIn(Control container)
        {
            RadComboBox ddl = new RadComboBox();
            container.Controls.Add(ddl);
            ddl.ID = ControlId;
            ddl.MarkFirstMatch = true;
            if (DropDownHeight > 0)
            {
                ddl.Height = DropDownHeight;
            }
            else
            {
                ddl.Height = 200;
            }
        }

        #region IBindableTemplate Members

        public System.Collections.Specialized.IOrderedDictionary ExtractValues(Control container)
        {
            OrderedDictionary od = new OrderedDictionary();
            RadComboBox ddl = (RadComboBox)container.FindControl(ControlId);
            int result;
            if (int.TryParse(ddl.SelectedValue, out result))
            {
                od.Add(DataField, result);
            }
            else
            {
                od.Add(DataField, null);
            }
            return od;
        }

        #endregion
    }
}