using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CRM.Core;
using Framework.Core;
using SFA.Web.Common;
using SubjectEngine.Component.Dto;

namespace SFA.Web.Areas.Admin.Models
{
    public class BaseViewModel
    {
        public SubjectDto Subject { get; set; }
        public object CurrentLanguageId { get; set; }

        public BaseViewModel(InstanceTypes instanceType)
        {
            Subject = WebContext.Current.SubjectList[instanceType.ToString()];
        }
    }
}