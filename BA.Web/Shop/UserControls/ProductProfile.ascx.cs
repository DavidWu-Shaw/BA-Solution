using System;
using BA.Web.Common;
using CRM.ShopComponent.Dto;

namespace BA.Web.Shop.UserControls
{
    public partial class ProductProfile : BaseControl
    {
        public ProductInfoDto Instance { get; set; }
        public bool HideSupplier { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void LoadProfile()
        {
            imgProduct.DataValue = Instance.Sketch;
            lblName.Text = Instance.Name;
            lblPrice.Text = Instance.UnitPrice.ToString();
            lblSupplierName.Text = Instance.SupplierName;
            if (HideSupplier)
            {
                lblSupplierName.Visible = false;
            }
            lblDescription.Text = Instance.Description;
            Rating1.Value = Instance.Rating;
            lblNumberOfRatings.Text = Instance.NumberOfRatings.ToString();
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblCount.Text = Localize("Shop.UserControls.ProductProfile.lblCount", "Number of ratings:");
        }
    }
}