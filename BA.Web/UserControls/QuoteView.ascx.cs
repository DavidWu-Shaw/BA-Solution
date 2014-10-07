using System;
using BA.Web.Common;
using CRM.Core;
using CRM.ShopComponent.Dto;

namespace BA.Web.UserControls
{
    public partial class QuoteView : BaseControl
    {
        public const string ControlURL = "/UserControls/QuoteView.ascx";

        public string Title { get; set; }
        public QuoteInfoDto Quote { get; set; }

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

        public void LoadData(QuoteInfoDto quote)
        {
            Quote = quote;
            LoadData();
        }

        private void LoadData()
        {
            if (Quote.QuoteId != null)
            {
                vQuoteNo.Text = Quote.QuoteId.ToString();
                vStatus.Text = ((QuoteStatuses)Quote.StatusId).ToString();
            }
            vTimeCreated.Text = Quote.TimeCreated.ToString(WebContext.Current.ApplicationOption.DateTimeFormat);
            vEmail.Text = Quote.Email;
            vContact.Text = Quote.ContactPerson;
            vPhone.Text = Quote.Phone;
            vAddress.Text = Quote.Address;
            vZipCode.Text = Quote.ZipCode;
            vNotes.Text = Quote.Notes;
            vProdName.Text = Quote.ProductName;
            vAmount.Text = Quote.Amount.ToString();
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblQuoteNo.Text = Localize("UserControls.QuoteView.lblQuoteNo", "Quote Number:");
            lblStatus.Text = Localize("UserControls.QuoteView.lblStatus", "Status:");
            lblTimeCreated.Text = Localize("UserControls.QuoteView.lblTimeCreated", "Created Time:");
            lblSubTitle.Text = Localize("UserControls.QuoteView.lblSubTitle", "Quote Information");
            lblSubTitle1.Text = Localize("UserControls.QuoteView.lblSubTitle1", "Contact Information");
            lblSubTitle2.Text = Localize("UserControls.QuoteView.lblSubTitle2", "Product Information");
            lbEmail.Text = Localize("UserControls.QuoteView.lbEmail", "Contact Email:");
            lbContact.Text = Localize("UserControls.QuoteView.lbContact", "Contact Person:");
            lbPhone.Text = Localize("UserControls.QuoteView.lbPhone", "Contact Phone:");
            lbZipCode.Text = Localize("UserControls.QuoteView.lbZipCode", "Post Code:");
            lbAddress.Text = Localize("UserControls.QuoteView.lbShipToAddress", "Address:");
            lbNotes.Text = Localize("UserControls.QuoteView.lbNotes", "Notes:");
            lblProdName.Text = Localize("UserControls.QuoteView.lblProdName", "Product:");
            lblAmount.Text = Localize("UserControls.QuoteView.lblAmount", "Amount:");
        }
    }
}