using System;
using BA.Web.Common;

namespace BA.Web.Shop
{
    public partial class MonoProductPage : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/MonoProductPage.aspx";
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
            PageName = Localize("Shop.MonoProductPage.PageName", "Product Detail");

            if (!IsPostBack)
            {
                ucProductDetail.InstanceId = InstanceId;
                ucProductDetail.HideSupplier = true;
            }
        }
    }
}