using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PA.Core
{
    public enum TaskType
    {
        Standard = 1,
        Meeting = 2,
        Milestone = 3,
        Event = 4,
    }

    public enum TaskStatus
    {
        Unassigned = 1,
        Assigned = 2,
        //Accepted = 3, 
        //Declined = 4, 
        //InProgress = 5,
        //Hold = 6, 
        Completed = 7,
    }

    public enum ProjectPermissionLevel
    {
        Read = 1,   // view project infomation, task list,
        Edit = 2,   // add task, edit task
        Admin = 3,  // add member, 
    }

    public enum TaskPermissionLevel
    {
        Read = 1,   // view task info
        Change = 2, // change task status
        Admin = 3,  // edit task, delete task, re-assign, change status
    }

    //public enum DocumentType 
    //{ 
    //    JPG = 1, 
    //    GIF = 2, 
    //    BMP = 3, 
    //    TIF = 4, 
    //    DWG = 5, 
    //    AI = 6, 
    //    EXCEL = 7, 
    //    Video = 8, 
    //    PDF = 9, 
    //    WORD = 10, 
    //    Compressed = 11, 
    //    TXT = 12, 
    //    Unknown = 99 
    //}

    //public enum UserStatus
    //{
    //    SignUpRequested = 1,
    //    SignUpConfirmed = 2,
    //    Login = 3,
    //    Logout = 4,
    //    Active = 5,
    //    Expired = 6,
    //}

    public class CommonDef
    {
        //public static readonly Color OverdueTaskColor = Color.Red;
        //public static readonly Color AtRiskTaskColor = Color.FromName("#FF6600");
        //public static readonly Color NormalTaskColor = Color.DarkGreen;
        //public static readonly Color CompletedTaskColor = Color.Blue;
        //public static readonly Color MilestoneTaskColor = Color.Gainsboro;

        // ChangedDate.ToString(CommonDef.DateTimeFormat);
        public const string DateTimeFormat = "MMM/dd/yyyy hh:mm";
        public const string DateFormat = "MMM/dd/yyyy";

    }
}
