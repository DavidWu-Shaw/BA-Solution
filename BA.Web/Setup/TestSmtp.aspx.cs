using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BA.Web.Common;
using System.Net.Mail;
using System.Net;

namespace BA.Web.Setup
{
    public partial class TestSmtp : AdminSetupBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            txtSmtpServer.Text = WebContext.Current.ApplicationOption.SMTPServer;
            txtPort.Text = WebContext.Current.ApplicationOption.SMTPPort.ToString();
            txtUser.Text = WebContext.Current.ApplicationOption.SMTPUsername;
            txtPasswd.Text = WebContext.Current.ApplicationOption.SMTPPassword;
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string smtpServer = txtSmtpServer.Text.Trim();
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            {
                port = 25;
            }
            string smtpUser = txtUser.Text.Trim();
            string smtpPasswd = txtPasswd.Text.Trim();
            string mailTo = txtMailTo.Text.Trim();

            SendEmail(smtpServer, port, smtpUser, smtpPasswd, mailTo);
        }

        private void SendEmail(string smtpServer, int port, string user, string passwd, string mailTo)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress("test@future.ca");
            message.To.Add(mailTo);
            message.Subject = "test subject";
            message.Body = "test body";

            SmtpClient smtpClient = new SmtpClient(smtpServer);
            smtpClient.Port = port;

            if (user.TrimHasValue() && passwd.TrimHasValue())
            {
                smtpClient.Credentials = new NetworkCredential(user, passwd, smtpServer);
            }
            else
            {
                smtpClient.UseDefaultCredentials = true;
            }

            try
            {
                smtpClient.Send(message);

                AlertMessages("Sending email successful.");
            }
            catch (SmtpFailedRecipientException ex)
            {
                ProcException(ex, "Sending email failed. ");
            }
            catch (SmtpException ex2)
            {
                ProcException(ex2, "Sending email failed. ");
            }
        }
    }
}