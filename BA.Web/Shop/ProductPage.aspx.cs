using System;
using BA.Web.Common;

namespace BA.Web.Shop
{
    public partial class ProductPage : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/ProductPage.aspx";
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
            PageName = Localize("Shop.ProductPage.PageName", "Product Detail");

            if (!IsPostBack)
            {
                ucProduct.InstanceId = InstanceId;
            }
        }
    }
}