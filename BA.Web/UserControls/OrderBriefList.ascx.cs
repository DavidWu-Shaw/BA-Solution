using System;
using System.Collections.Generic;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.ShopComponent.Dto;
using Telerik.Web.UI;

namespace BA.Web.UserControls
{
    public partial class OrderBriefList : BaseControl
    {
        public const string ControlURL = "/UserControls/OrderBriefList.ascx";

        public event SelectedInstanceChangedEventHandler SelectedInstanceChanged;

        public string Title { get; set; }

        public RadListBox RadListBoxControl
        {
            get
            {
                return lbOrders;
            }
        }

        public IEnumerable<OrderBriefInfoDto> Orders { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            InitControls();
            if (!IsPostBack)
            {
                LoadOrders();
            }
        }

        protected void lbOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedId;
            if (int.TryParse(lbOrders.SelectedValue, out selectedId))
            {
                if (SelectedInstanceChanged != null)
                {
                    SelectedInstanceChangedEventArgs arg = new SelectedInstanceChangedEventArgs(selectedId);
                    SelectedInstanceChanged(this, arg);
                }
            }
        }

        private void InitControls()
        {
            if (!string.IsNullOrEmpty(Title))
            {
                divHeader.Visible = true;
                lblTitle.Text = Title;
            }
            else
            {
                divHeader.Visible = false;
            }

            lbOrders.DataValueField = OrderBriefInfoDto.FLD_OrderId;
        }

        public void LoadOrders()
        {
            lbOrders.DataSource = Orders;
            lbOrders.DataBind();
        }

        public void ClearListSelection()
        {
            lbOrders.ClearSelection();
        }

        public string GetHeaderOfOrderNumber()
        {
            return Localize("UserControls.OrderBriefList.lblNo", "Order Number");
        }

        public string GetHeaderOfStatus()
        {
            return Localize("UserControls.OrderBriefList.lblStatus", "Status");
        }

        public string GetHeaderOfAmount()
        {
            return Localize("UserControls.OrderBriefList.lblAmount", "Amount");
        }
    }
}