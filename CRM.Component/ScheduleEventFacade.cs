using System;
using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    public class ScheduleEventFacade : BusinessComponent
    {
        public ScheduleEventFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
            ScheduleEventSystem = new ScheduleEventSystem(unitOfWork);
        }

        private ScheduleEventSystem ScheduleEventSystem { get; set; }

        public List<TDto> RetrieveAllScheduleEvent<TDto>(IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ScheduleEventSystem.RetrieveAllScheduleEvent(converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveScheduleEventsByCustomer<TDto>(object customerId, IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ScheduleEventSystem.RetrieveScheduleEventsByCustomer(customerId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public List<TDto> RetrieveScheduleEventsByContact<TDto>(object contactId, IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            List<TDto> instances = ScheduleEventSystem.RetrieveScheduleEventsByContact(contactId, converter);
            if (instances == null)
            {
                instances = new List<TDto>();
            }
            return instances;
        }

        public TDto RetrieveOrNewScheduleEvent<TDto>(object instanceId, IDataConverter<ScheduleEventData, TDto> converter)
            where TDto : class
        {
            return ScheduleEventSystem.RetrieveOrNewScheduleEvent(instanceId, converter);
        }

        public IFacadeUpdateResult<ScheduleEventData> SaveScheduleEvent(ScheduleEventDto dto)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ScheduleEventData> result = ScheduleEventSystem.SaveScheduleEvent(dto);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public IFacadeUpdateResult<ScheduleEventData> DeleteScheduleEvent(object id)
        {
            UnitOfWork.BeginTransaction();
            IFacadeUpdateResult<ScheduleEventData> result = ScheduleEventSystem.DeleteScheduleEvent(id);
            if (result.IsSuccessful)
            {
                UnitOfWork.CommitTransaction();
            }
            else
            {
                UnitOfWork.RollbackTransaction();
            }

            return result;
        }

        public List<CalendarEventDto> GetCalendarEvents(DateTime startDate, DateTime endDate)
        {
            Dictionary<string, string> objectDisplayTable = GetObjectDisplayTable();
            return ScheduleEventSystem.GetCalendarEvents(startDate, endDate, objectDisplayTable);
        }

        public List<CalendarEventDto> GetCalendarEvents(DateTime date)
        {
            Dictionary<string, string> objectDisplayTable = GetObjectDisplayTable();
            return ScheduleEventSystem.GetCalendarEvents(date, objectDisplayTable);
        }

        public List<CalendarEventDto> GetCalendarEventsByEmployee(object employeeId, DateTime startDate, DateTime endDate)
        {
            Dictionary<string, string> objectDisplayTable = GetObjectDisplayTable();
            return ScheduleEventSystem.GetCalendarEventsByEmployee(employeeId, startDate, endDate, objectDisplayTable);
        }

        public List<CalendarEventDto> GetCalendarEventsByEmployee(object employeeId, DateTime date)
        {
            Dictionary<string, string> objectDisplayTable = GetObjectDisplayTable();
            return ScheduleEventSystem.GetCalendarEventsByEmployee(employeeId, date, objectDisplayTable);
        }

        private Dictionary<string, string> GetObjectDisplayTable()
        {
            Dictionary<string, string> objectDisplayTable = new Dictionary<string, string>();

            // Add Customers
            CustomerSystem customerSystem = new CustomerSystem(UnitOfWork);
            AddToObjectDisplayTable(EventObjectTypes.Customer, customerSystem.GetBindingList(), objectDisplayTable);

            // Add Contacts
            ContactSystem contactSystem = new ContactSystem(UnitOfWork);
            AddToObjectDisplayTable(EventObjectTypes.Contact, contactSystem.GetBindingList(), objectDisplayTable);

            return objectDisplayTable;
        }

        private void AddToObjectDisplayTable(EventObjectTypes objectType, IList<BindingListItem> list, Dictionary<string, string> objectDisplayTable)
        {
            int objectTypeId = (int)objectType;
            foreach (BindingListItem item in list)
            {
                string key = string.Format(CommonConst.CombinationKeyFormatString, objectTypeId, item.Value);
                objectDisplayTable.Add(key, item.Text);
            }
        }
    }
}
