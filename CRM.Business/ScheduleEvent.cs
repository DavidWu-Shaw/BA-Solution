using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;
using CRM.Core;

namespace CRM.Business
{
    public class ScheduleEvent : BusinessObject<ScheduleEventData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            StartDate = DateTime.Today;
            ScheduledTime = DateTime.Now;
        }

        [RequiredField("ScheduleEventNameRequired", "The Name must be defined.")]
        [StringLength("ScheduleEventNameLength", "The Name must have a length less than {1}", MaxLength = 100)]
        public string Name
        {
            get
            {
                return Data.Name;
            }
            set
            {
                Data.Name = value;
            }
        }

        [RequiredField("ScheduleEventScheduledTimeRequired", "The ScheduledTime must be defined.")]
        public DateTime ScheduledTime
        {
            get
            {
                return Data.ScheduledTime;
            }
            set
            {
                Data.ScheduledTime = value;
            }
        }

        public object ObjectId
        {
            get { return Data.ObjectId; }
            set { Data.ObjectId = value; }
        }

        public int ObjectTypeId
        {
            get { return Data.ObjectTypeId; }
            set { Data.ObjectTypeId = value; }
        }

        public int ReccuringTypeId
        {
            get { return Data.ReccuringTypeId; }
            set { Data.ReccuringTypeId = value; }
        }

        public DateTime StartDate
        {
            get { return Data.StartDate; }
            set { Data.StartDate = value; }
        }

        public DateTime? EndDate
        {
            get { return Data.EndDate; }
            set { Data.EndDate = value; }
        }

        public bool IsOccuring(DateTime currentDate)
        {
            bool isOccuring = false;

            if ((currentDate < StartDate) || (EndDate.HasValue && currentDate > EndDate.Value))
            {
                // ScheduledEvent expired
                isOccuring = false;
            }
            else
            {
                EventReccuringTypes ReccuringType = (EventReccuringTypes)ReccuringTypeId;

                switch (ReccuringType)    
                {
                    case EventReccuringTypes.Yearly:
                        if (currentDate.DayOfYear == ScheduledTime.DayOfYear)
                        {
                            isOccuring = true;
                        }
                        break;
                    case EventReccuringTypes.Monthly:
                        if (currentDate.Day == ScheduledTime.Day)
                        {
                            isOccuring = true;
                        }
                        break;
                    case EventReccuringTypes.Weekly:
                        if (currentDate.DayOfWeek == ScheduledTime.DayOfWeek)
                        {
                            isOccuring = true;
                        }
                        break;
                    case EventReccuringTypes.Daily:
                        isOccuring = true;
                        break;
                    case EventReccuringTypes.Once:
                        if (currentDate.Date == ScheduledTime.Date)
                        {
                            isOccuring = true;
                        }
                        break;
                }
            }


            return isOccuring;
        }
    }
}
