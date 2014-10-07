using System;
using CRM.ShopComponent.Dto;
using BA.Web.Common;

namespace BA.Web.UserControls
{
    public partial class OrderView : BaseControl
    {
        public const string ControlURL = "/UserControls/OrderView.ascx";

        public string Title { get; set; }
        public OrderInfoDto Order { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControls();
        }

        private void InitControls()
        {
            if (!string.IsNullOrEmpty(Title))
            {
                divHeader.Visible = true;
                lbTitle.Text = Title;
            }
            else
            {
                divHeader.Visible = false;
            }
        }

        public void LoadData(OrderInfoDto order)
        {
            Order = order;
            LoadData();
        }

        private void LoadData()
        {
            vOrderNo.Text = Order.OrderNumber;
            vStatus.Text = Order.StatusText;
            vSupplier.Text = Order.SupplierName;
            vShipToContact.Text = Order.ShipTo.ContactPerson;
            vShipToPhone.Text = Order.ShipTo.ContactPhone;
            vShipToAddress.Text = Order.ShipTo.AddressLine1;
            vShipToZipCode.Text = Order.ShipTo.ZipCode;
            if (Order.TimeShipBy.HasValue)
            {
                vShipBy.Text = Order.TimeShipBy.ToString();
            }
            vNotes.Text = Order.Notes;

            ucItems.LoadData(Order);
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lbOrderNo.Text = Localize("UserControls.OrderView.lbOrderNo", "Order Number:");

            lbStatus.Text = Localize("UserControls.OrderView.lbStatus", "Order Status:");
            lbSupplier.Text = Localize("UserControls.OrderView.lbSupplier", "Supplier:");
            lbShipToContact.Text = Localize("UserControls.OrderView.lbShipToContact", "Ship To Contact:");
            lbShipToPhone.Text = Localize("UserControls.OrderView.lbShipToPhone", "Ship To Phone:");
            lbShipToAddress.Text = Localize("UserControls.OrderView.lbShipToAddress", "Ship To Address:");
            lbShipToZipCode.Text = Localize("UserControls.OrderView.lbShipToZipCode", "Post Code:");
            lbShipBy.Text = Localize("UserControls.OrderView.lbShipBy", "Ship By Time:");
            lbNotes.Text = Localize("UserControls.OrderView.lbNotes", "Notes:");
        }
    }
}