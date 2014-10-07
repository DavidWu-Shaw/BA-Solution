using System;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.ShopComponent.Dto;

namespace BA.Web.Shop
{
    public partial class ContactUsPage : AnonymousBasePage
    {
        public const string PageUrl = @"/Shop/ContactUsPage.aspx";

        protected override void OnInit(EventArgs e)
        {
            RequireSSL = true;
            base.OnInit(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Shop.ContactUsPage.PageName", "Contact Us");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string subject = txtSubject.Text.Trim();
            if (email.Length > 0 && subject.Length > 0)
            {
                ContactUsInfoDto contactUsInfo = new ContactUsInfoDto();
                contactUsInfo.Email = email;
                contactUsInfo.Subject = subject;
                contactUsInfo.Name = txtName.Text.Trim();
                contactUsInfo.Phone = txtPhone.Text.Trim();
                contactUsInfo.Address = txtAddress.Text.Trim();
                contactUsInfo.Message = txtMessage.Text.Trim();
                contactUsInfo.IssuedTime = DateTime.Now;

                try
                {
                    NotificationProcessor.SendContactUs(contactUsInfo);
                    lbResultMsg.Text = Localize("Shop.ContactUsPage.lbResultMsg", "Thank you for contacting us! Your message has been sent to our support team.");
                    ClearForm();
                }
                catch (Exception ex)
                {
                    ProcException(ex, "Sending email failed. ");
                }
            }
        }

        private void ClearForm()
        {
            txtEmail.Text = string.Empty;
            txtName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtMessage.Text = string.Empty;
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();

            lblTitle.Text = Localize("Shop.ContactUsPage.lblTitle", "Please fill out following form to contact us.");
            lblEmail.Text = Localize("Shop.ContactUsPage.lblEmail", "Email Address:");
            lblName.Text = Localize("Shop.ContactUsPage.lblName", "Name:");
            lblPhone.Text = Localize("Shop.ContactUsPage.lblPhone", "Phone:");
            lblAddress.Text = Localize("Shop.ContactUsPage.lblAddress", "Address:");
            lblSubject.Text = Localize("Shop.ContactUsPage.lblSubject", "Subject:");
            lblMessage.Text = Localize("Shop.ContactUsPage.lblMessage", "Message:");
            btnSubmit.Text = Localize("Shop.ContactUsPage.btnSubmit", "Submit");
        }
    }
}