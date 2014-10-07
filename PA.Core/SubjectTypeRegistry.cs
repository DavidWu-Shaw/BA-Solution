using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PA.Core
{
    public static class SubjectTypeRegistry
    {
        // These values must strictly match tblSubject->SubjectType
        public const string User = "User";
        public const string Project = "Project";
        public const string Task = "Task";
        public const string ProjectMember = "ProjectMember";
        public const string TaskComment = "TaskComment";
        public const string TaskTime = "TaskTime";
    }
}
