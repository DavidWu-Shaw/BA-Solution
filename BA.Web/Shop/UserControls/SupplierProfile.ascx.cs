using System;
using BA.Web.Common;
using CRM.ShopComponent.Dto;

namespace BA.Web.Shop.UserControls
{
    public partial class SupplierProfile : BaseControl
    {
        public SupplierInfoDto Instance { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void ProfileDataBind()
        {
            lblName.Text = Instance.Name;
            if (Instance.Logo != null)
            {
                imgSupplier.DataValue = Instance.Logo;
            }
            else
            {
                imgSupplier.DataValue = new byte[0];
            }
            lblAddress.Text = Instance.Address;
            lblCity.Text = Instance.City;
            lblZipCode.Text = Instance.ZipCode;
            lblPhone.Text = Instance.ContactPhone;
            lblWebsite.Text = Instance.Website;
        }
    }
}