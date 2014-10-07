using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class MySchedulerProvider : SchedulerProviderBase
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        private UserContext CurrentUserContext { get; set; }

        public MySchedulerProvider()
        {
        }

        public MySchedulerProvider(UserContext user)
        {
            CurrentUserContext = user;
        }

        private IEnumerable<CalendarEventDto> CalendarEvents { get; set; }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ScheduleEventFacade facade = new ScheduleEventFacade(uow);
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    if (CurrentUserContext != null && CurrentUserContext.IsEmployee)
                    {
                        CalendarEvents = facade.GetCalendarEventsByEmployee(CurrentUserContext.User.MatchId, StartDate.Value, EndDate.Value);
                    }
                    else
                    {
                        CalendarEvents = facade.GetCalendarEvents(StartDate.Value, EndDate.Value);
                    }
                }
                else if (StartDate.HasValue)
                {
                    if (CurrentUserContext != null && CurrentUserContext.IsEmployee)
                    {
                        CalendarEvents = facade.GetCalendarEventsByEmployee(CurrentUserContext.User.MatchId, StartDate.Value);
                    }
                    else
                    {
                        CalendarEvents = facade.GetCalendarEvents(StartDate.Value);
                    }
                }
                else
                {
                    CalendarEvents = new List<CalendarEventDto>();
                }
            }
        }

        public override IEnumerable<Appointment> GetAppointments(RadScheduler owner)
        {
            RetrieveData();

            List<Appointment> appointments = new List<Appointment>();

            int index = 1;
            foreach (CalendarEventDto calendarEvent in CalendarEvents)
            {
                Appointment apt = new Appointment();
                apt.Owner = owner;
                apt.ID = index++;
                apt.Subject = string.Format("{0} ({1}, {2:t})", calendarEvent.EventSubject, calendarEvent.ObjectDisplay, calendarEvent.ScheduledTime);
                apt.Start = calendarEvent.ScheduledTime;
                if (calendarEvent.ScheduledTime.Hour == 0 && calendarEvent.ScheduledTime.Minute == 0)
                {
                    apt.End = calendarEvent.ScheduledTime.AddDays(1);
                }
                else
                {
                    apt.End = calendarEvent.ScheduledTime.AddHours(0.5);
                }
                apt.RecurrenceRule = string.Empty;
                apt.RecurrenceParentID = null;

                appointments.Add(apt);
            }

            return appointments;
        }

        public override IDictionary<ResourceType, IEnumerable<Resource>> GetResources(ISchedulerInfo schedulerInfo)
        {
            return null;
        }

        public override IEnumerable<Resource> GetResourcesByType(RadScheduler owner, string resourceType)
        {
            return null;
        }

        public override IEnumerable<ResourceType> GetResourceTypes(RadScheduler owner)
        {
            return null;
        }
    }
}