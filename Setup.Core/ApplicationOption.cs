using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Setup.Core
{
    public class ApplicationOption
    {
        public ApplicationOption()
        {
            Name = "App";
            IsTestMode = false;
            EnableSSL = false;
            EnableMultiUser = false;
            EnableRegister = false;
            IsMultiLanguageSupported = true;
            DefaultLanguageId = 1;
            IsShoppingSupported = true;
            IsQuoteSupported = true;
            EnableGridColumnFilter = false;
            EnableNotification = false;
            SchedulerDefaultView = SchedulerViewTypes.Week;
            DayStartTime = new TimeSpan(8, 0, 0);
            DayEndTime = new TimeSpan(21, 0, 0);
            DropDownHeight = 200;
            GridPageSize = 20;
            EditFormColumnMax = 2;
            NewsContentBriefLength = 100;
            DateFormat = "yyyy-MMM-dd";
            DateTimeFormat = "yyyy-MMM-dd HH:mm";
            TimeFormat = "HH:mm";
            DateFormatString = "{0: yyyy-MMM-dd}";
            DateTimeFormatString = "{0: yyyy-MMM-dd HH:mm}";
            TimeFormatString = "{0: HH:mm}";
            SMTPServer = "SMTP.CA";
            SMTPPort = 25;
            SiteCoordinatorEmail = "david.wu@future.ca";
            QuoteCoordinatorEmail = "david.wu@future.ca";
            OrderCoordinatorEmail = "david.wu@future.ca";
            DefaultEmailFrom = "test@test.com";
            IsEmailToCustomer = true;
            IsEmailToSupplier = true;
            IsSetSOHold = false;
            IsAccountActivateRequired = false;
            OrderTrackingUrlFormat = "http://host/Shop/OrderTracking.aspx?OrderNo={0}";
        }

        public string Name { get; set; }
        public bool IsTestMode { get; set; }
        public bool EnableSSL { get; set; }
        public string Version { get; set; }
        public bool EnableMultiUser { get; set; }
        public bool EnableRegister { get; set; }
        public bool IsMultiLanguageSupported { get; set; }
        public object DefaultLanguageId { get; set; }
        public bool IsShoppingSupported { get; set; }
        public bool IsQuoteSupported { get; set; }
        public bool EnableGridColumnFilter { get; set; }
        public bool EnableNotification { get; set; }
        public SchedulerViewTypes SchedulerDefaultView { get; set; }
        public TimeSpan DayStartTime { get; set; }
        public TimeSpan DayEndTime { get; set; }
        public int DropDownHeight { get; set; }
        public int GridPageSize { get; set; }
        public int EditFormColumnMax { get; set; }
        public int NewsContentBriefLength { get; set; }
        public string DateTimeFormat { get; set; }
        public string DateFormat { get; set; }
        public string TimeFormat { get; set; }
        public string DateTimeFormatString { get; set; }
        public string DateFormatString { get; set; }
        public string TimeFormatString { get; set; }
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPUsername { get; set; }
        public string SMTPPassword { get; set; }
        public string SiteCoordinatorEmail { get; set; }
        public string QuoteCoordinatorEmail { get; set; }
        public string OrderCoordinatorEmail { get; set; }
        public string DefaultEmailFrom { get; set; }
        public bool IsEmailToCustomer { get; set; }
        public bool IsEmailToSupplier { get; set; }
        public bool IsSetSOHold { get; set; }
        public string OrderTrackingUrlFormat { get; set; }
        public bool IsAccountActivateRequired { get; set; }
        public object GlobalProductCatalogId { get; set; }
        public object SupplierCatalogId { get; set; }
    }
}
