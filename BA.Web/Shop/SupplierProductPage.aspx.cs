using System;
using System.Web;
using BA.Web.Common;

namespace BA.Web.Shop
{
    public partial class SupplierProductPage : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/SupplierProductPage.aspx";
        public const string QryInstanceId = "ProductId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryInstanceId].TryToParse<int>();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.SupplierProductPage.PageName", "Product home");

            SiteMap.SiteMapResolve += new SiteMapResolveEventHandler(SiteMap_SiteMapResolve);

            if (!IsPostBack)
            {
                ucProduct.InstanceId = InstanceId;
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            SiteMap.SiteMapResolve -= SiteMap_SiteMapResolve;
            base.OnPreRenderComplete(e);
        }

        SiteMapNode SiteMap_SiteMapResolve(object sender, SiteMapResolveEventArgs e)
        {
            SiteMapNode currentNode = SiteMap.CurrentNode.Clone(true);
            SiteMapNode tempNode = currentNode;
            tempNode.Title = ucProduct.CurrentInstance.Name;
            tempNode = tempNode.ParentNode;
            if ((tempNode != null) && (ucProduct.CurrentInstance.SupplierId != null))
            {
                tempNode.Title = ucProduct.CurrentInstance.SupplierName;
                tempNode.Url = string.Format("{0}?{1}={2}", tempNode.Url, SupplierPage.QryInstanceId, ucProduct.CurrentInstance.SupplierId);
            }
            return currentNode;
        }
    }
}