using System;
using BA.Web.Common.Helper;
using Setup.Core;
using Telerik.Web.UI;
using Telerik.Web.UI.Calendar;

namespace BA.Web.Common.UserControls
{
    public partial class SchedulerControl : System.Web.UI.UserControl
    {
        public MySchedulerProvider DataProvider { get; set; }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitScheduler();
        }

        private void InitScheduler()
        {
            RadCalendar1.SelectionChanged += new SelectedDatesEventHandler(RadCalendar1_SelectionChanged);
            RadCalendar1.DefaultViewChanged += new DefaultViewChangedEventHandler(RadCalendar1_DefaultViewChanged);
            RadCalendar2.SelectionChanged += new SelectedDatesEventHandler(RadCalendar2_SelectionChanged);
            RadScheduler1.NavigationCommand += new SchedulerNavigationCommandEventHandler(RadScheduler1_NavigationCommand);

            switch (WebContext.Current.ApplicationOption.SchedulerDefaultView)
            {
                case SchedulerViewTypes.Week:
                    RadScheduler1.SelectedView = SchedulerViewType.WeekView;
                    break;
                case SchedulerViewTypes.Month:
                    RadScheduler1.SelectedView = SchedulerViewType.MonthView;
                    break;
                case SchedulerViewTypes.Day:
                    RadScheduler1.SelectedView = SchedulerViewType.DayView;
                    break;
                default:
                    RadScheduler1.SelectedView = SchedulerViewType.WeekView;
                    break;
            }
            RadScheduler1.DayStartTime = WebContext.Current.ApplicationOption.DayStartTime;
            RadScheduler1.DayEndTime = WebContext.Current.ApplicationOption.DayEndTime;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RadScheduler1.Provider = DataProvider;

            if (!IsPostBack)
            {
                RadCalendar1.SelectedDate = RadScheduler1.SelectedDate;
                SyncCalendars();
            }
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            DataProvider.StartDate = RadScheduler1.SelectedDate.AddDays(-31);
            DataProvider.EndDate = RadScheduler1.SelectedDate.AddDays(31);
        }

        protected void RadCalendar1_DefaultViewChanged(object sender, DefaultViewChangedEventArgs e)
        {
            SyncCalendars();
        }

        protected void RadCalendar1_SelectionChanged(object sender, SelectedDatesEventArgs e)
        {
            if (RadCalendar1.SelectedDates.Count > 0)
            {
                RadScheduler1.SelectedDate = RadCalendar1.SelectedDate;
                RadCalendar2.SelectedDate = RadCalendar1.SelectedDate;
            }
        }

        protected void RadCalendar2_SelectionChanged(object sender, SelectedDatesEventArgs e)
        {
            if (RadCalendar2.SelectedDates.Count > 0)
            {
                RadScheduler1.SelectedDate = RadCalendar2.SelectedDate;
                RadCalendar1.SelectedDate = RadCalendar2.SelectedDate;
            }
        }

        protected void RadScheduler1_NavigationCommand(object sender, SchedulerNavigationCommandEventArgs e)
        {
            if (e.Command == SchedulerNavigationCommand.SwitchToSelectedDay)
            {
                RadCalendar1.SelectedDate = e.SelectedDate;
                SyncCalendars();
            }
        }

        private void SyncCalendars()
        {
            RadCalendar2.FocusedDate = RadCalendar1.FocusedDate.AddMonths(1);
        }
    }
}