using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class HyperLinkItemTemplate : ITemplate
    {
        public string LinkTextField { get; set; }
        public string Text { get; set; }
        public string NavigateUrlField { get; set; }
        public string NavigateUrlFormatString { get; set; }

        #region ITemplate Members

        public void InstantiateIn(Control container)
        {
            HyperLink linkButton = new HyperLink();
            linkButton.DataBinding += new EventHandler(linkButton_DataBinding);
            container.Controls.Add(linkButton);
        }

        #endregion

        private void linkButton_DataBinding(object sender, EventArgs e)
        {
            HyperLink hyperLink = (HyperLink)sender;
            GridItem container = (GridItem)hyperLink.NamingContainer;
            object dataUrl = DataBinder.Eval(container.DataItem, NavigateUrlField);
            if (!string.IsNullOrEmpty(LinkTextField))
            {
                object dataText = DataBinder.Eval(container.DataItem, LinkTextField);
                Text = dataText.ToString();
            }
            hyperLink.Text = Text;
            hyperLink.NavigateUrl = string.Format(NavigateUrlFormatString, dataUrl);
        }
    }
}