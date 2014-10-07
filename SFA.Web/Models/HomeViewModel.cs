using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SFA.Web.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
        }

        public string CaptchaText { get; set; }

        public string CurrentAction { get; set; }

        public MvcHtmlString CurrentContent { get; set; }

        public MvcHtmlString CurrentContentDetail { get; set; }

        public string ContactUsEmail { get; set; }

        public string ContactUsMessage { get; set; }

        public string ContactUsSubject { get; set; }

        public bool IsContactUsNotificationSent { get; set; }
    }
}