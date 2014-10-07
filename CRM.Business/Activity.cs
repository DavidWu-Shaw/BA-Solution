using System;
using Framework.Business;
using CRM.Data;
using Framework.Validation;

namespace CRM.Business
{
    public class Activity : BusinessObject<ActivityData>
    {
        protected override void SetDefaultValues()
        {
            base.SetDefaultValues();

            StartTime = DateTime.Now;
        }

        [RequiredField("ActivityActivityNameRequired", "The ActivityName must be defined.")]
        [StringLength("ActivityActivityNameLength", "The ActivityName must have a length less than {1}", MaxLength = 100)]
        public string ActivityName
        {
            get
            {
                return Data.ActivityName;
            }
            set
            {
                Data.ActivityName = value;
            }
        }

        public object EmployeeId
        {
            get { return Data.EmployeeId; }
            set { Data.EmployeeId = value; }
        }

        public object CustomerId
        {
            get { return Data.CustomerId; }
            set { Data.CustomerId = value; }
        }

        public object ContactId
        {
            get { return Data.ContactId; }
            set { Data.ContactId = value; }
        }

        public object ActivityTypeId
        {
            get { return Data.ActivityTypeId; }
            set { Data.ActivityTypeId = value; }
        }

        [StringLength("ActivityNotesLength", "The Notes must have a length less than {1}", MaxLength = 500)]
        public string Notes
        {
            get
            {
                return Data.Notes;
            }
            set
            {
                Data.Notes = value;
            }
        }

        public DateTime StartTime
        {
            get
            {
                return Data.StartTime;
            }
            set
            {
                Data.StartTime = value;
            }
        }

        public DateTime? EndTime
        {
            get
            {
                return Data.EndTime;
            }
            set
            {
                Data.EndTime = value;
            }
        }

        public decimal TimeSpent
        {
            get
            {
                return Data.TimeSpent;
            }
            set
            {
                Data.TimeSpent = value;
            }
        }
    }
}
