using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class ScheduleEventSystem : BusinessComponent
    {
        public ScheduleEventSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllScheduleEvent<TDto>(IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveScheduleEventsByCustomer<TDto>(object customerId, IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("customerId", customerId);
            ArgumentValidator.IsNotNull("converter", converter);
            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();

            var query = service.SearchByCustomer(customerId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveScheduleEventsByContact<TDto>(object contactId, IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("contactId", contactId);
            ArgumentValidator.IsNotNull("converter", converter);
            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();

            var query = service.SearchByContact(contactId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewScheduleEvent<TDto>(object instanceId, IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();
            FacadeUpdateResult<ScheduleEventData> result = new FacadeUpdateResult<ScheduleEventData>();
            ScheduleEvent instance = RetrieveOrNew<ScheduleEventData, ScheduleEvent, IScheduleEventService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<ScheduleEventData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<ScheduleEventData> SaveScheduleEvent(ScheduleEventDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<ScheduleEventData> result = new FacadeUpdateResult<ScheduleEventData>();
            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();
            ScheduleEvent instance = RetrieveOrNew<ScheduleEventData, ScheduleEvent, IScheduleEventService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.ScheduledTime = dto.ScheduledTime;
                instance.ObjectId = dto.ObjectId;
                instance.ObjectTypeId = dto.ObjectTypeId;
                instance.ReccuringTypeId = dto.ReccuringTypeId;
                instance.StartDate = dto.StartDate;
                instance.EndDate = dto.EndDate;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<ScheduleEventData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        internal IFacadeUpdateResult<ScheduleEventData> DeleteScheduleEvent(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<ScheduleEventData> result = new FacadeUpdateResult<ScheduleEventData>();
            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                ScheduleEvent instance = query.ToBo<ScheduleEvent>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "ScheduleEventCannotBeFound");
            }

            return result;
        }

        internal List<CalendarEventDto> GetCalendarEvents(DateTime startDate, DateTime endDate, Dictionary<string, string> objectDisplayTable)
        {
            ArgumentValidator.IsNotNull("startDate", startDate);
            ArgumentValidator.IsNotNull("endDate", endDate);

            List<CalendarEventDto> calendarEvents = new List<CalendarEventDto>();

            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                IEnumerable<ScheduleEvent> scheduleEvents = query.ToBoList<ScheduleEvent>();
                foreach (ScheduleEvent scheduleEvent in scheduleEvents)
                {
                    List<CalendarEventDto> events = GetCalendarEvents(scheduleEvent, startDate, endDate, objectDisplayTable);
                    calendarEvents.AddRange(events);
                }
            }

            return calendarEvents;
        }

        internal List<CalendarEventDto> GetCalendarEvents(DateTime date, Dictionary<string, string> objectDisplayTable)
        {
            ArgumentValidator.IsNotNull("date", date);

            List<CalendarEventDto> calendarEvents = new List<CalendarEventDto>();

            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                IEnumerable<ScheduleEvent> scheduleEvents = query.ToBoList<ScheduleEvent>();
                foreach (ScheduleEvent scheduleEvent in scheduleEvents)
                {
                    CalendarEventDto calendarEvent = GetCalendarEvent(scheduleEvent, date, objectDisplayTable);
                    if (calendarEvent != null)
                    {
                        calendarEvents.Add(calendarEvent);
                    }
                }
            }

            return calendarEvents;
        }

        internal List<CalendarEventDto> GetCalendarEventsByEmployee(object employeeId, DateTime startDate, DateTime endDate, Dictionary<string, string> objectDisplayTable)
        {
            ArgumentValidator.IsNotNull("employeeId", employeeId);
            ArgumentValidator.IsNotNull("startDate", startDate);
            ArgumentValidator.IsNotNull("endDate", endDate);

            List<CalendarEventDto> calendarEvents = new List<CalendarEventDto>();

            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();
            var query = service.SearchByEmployee(employeeId);
            if (query.HasResult)
            {
                IEnumerable<ScheduleEvent> scheduleEvents = query.ToBoList<ScheduleEvent>();
                foreach (ScheduleEvent scheduleEvent in scheduleEvents)
                {
                    List<CalendarEventDto> events = GetCalendarEvents(scheduleEvent, startDate, endDate, objectDisplayTable);
                    calendarEvents.AddRange(events);
                }
            }

            return calendarEvents;
        }

        internal List<CalendarEventDto> GetCalendarEventsByEmployee(object employeeId, DateTime date, Dictionary<string, string> objectDisplayTable)
        {
            ArgumentValidator.IsNotNull("employeeId", employeeId);
            ArgumentValidator.IsNotNull("date", date);

            List<CalendarEventDto> calendarEvents = new List<CalendarEventDto>();

            IScheduleEventService service = UnitOfWork.GetService<IScheduleEventService>();
            var query = service.SearchByEmployee(employeeId);
            if (query.HasResult)
            {
                IEnumerable<ScheduleEvent> scheduleEvents = query.ToBoList<ScheduleEvent>();
                foreach (ScheduleEvent scheduleEvent in scheduleEvents)
                {
                    CalendarEventDto calendarEvent = GetCalendarEvent(scheduleEvent, date, objectDisplayTable);
                    if (calendarEvent != null)
                    {
                        calendarEvents.Add(calendarEvent);
                    }
                }
            }

            return calendarEvents;
        }
        # region Helper methods

        private List<CalendarEventDto> GetCalendarEvents(ScheduleEvent scheduleEvent, DateTime startDate, DateTime endDate, Dictionary<string, string> objectDisplayTable)
        {
            List<CalendarEventDto> calendarEvents = new List<CalendarEventDto>();

            for (DateTime dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                CalendarEventDto calendarEvent = GetCalendarEvent(scheduleEvent, dt, objectDisplayTable);
                if (calendarEvent != null)
                {
                    calendarEvents.Add(calendarEvent);
                }
            }

            return calendarEvents;
        }

        private CalendarEventDto GetCalendarEvent(ScheduleEvent scheduleEvent, DateTime date, Dictionary<string, string> objectDisplayTable)
        {
            if (scheduleEvent.IsOccuring(date))
            {
                DateTime eventTime = new DateTime(date.Year, date.Month, date.Day, scheduleEvent.ScheduledTime.Hour, scheduleEvent.ScheduledTime.Minute, scheduleEvent.ScheduledTime.Second);
                CalendarEventDto calendarEvent = new CalendarEventDto();
                calendarEvent.EventSubject = scheduleEvent.Name;
                calendarEvent.ScheduledTime = eventTime;
                calendarEvent.ObjectType = ((EventObjectTypes)scheduleEvent.ObjectTypeId).ToString();
                calendarEvent.ObjectId = scheduleEvent.ObjectId;
                string key = string.Format(CommonConst.CombinationKeyFormatString, scheduleEvent.ObjectTypeId, scheduleEvent.ObjectId);
                if (objectDisplayTable.ContainsKey(key))
                {
                    calendarEvent.ObjectDisplay = objectDisplayTable[key];
                }
                calendarEvent.ScheduleEventId = scheduleEvent.Id;
                return calendarEvent;
            }
            else
            {
                return null;
            }
        }

        # endregion Helper methods
    }
}
