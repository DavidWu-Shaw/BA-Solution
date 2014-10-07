using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class PopupButtonItemTemplate : ITemplate
    {
        public string DataTextField { get; set; }
        public string PopupUrlField { get; set; }
        public string PopupUrlFormatString { get; set; }

        public PopupButtonItemTemplate(string dataField)
        {
            DataTextField = dataField;
        }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            HyperLink linkButton = new HyperLink();
            linkButton.NavigateUrl = "javascript: void(0);";
            linkButton.DataBinding += new EventHandler(linkButton_DataBinding);
            container.Controls.Add(linkButton);
        }

        #endregion

        private void linkButton_DataBinding(object sender, EventArgs e)
        {
            HyperLink lnkButton = (HyperLink)sender;
            GridItem container = (GridItem)lnkButton.NamingContainer;
            object dataText = DataBinder.Eval(container.DataItem, DataTextField);
            object dataUrl = DataBinder.Eval(container.DataItem, PopupUrlField);
            if (dataText != null)
            {
                lnkButton.Text = dataText.ToString();
                lnkButton.Attributes.Add("onclick", string.Format(PopupUrlFormatString, dataUrl));
            }
        }
    }
}