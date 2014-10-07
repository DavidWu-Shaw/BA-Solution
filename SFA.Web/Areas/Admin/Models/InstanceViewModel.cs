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
    public class InstanceViewModel : BaseViewModel
    {
        public BaseDto Instance { get; set; }

        public InstanceViewModel(InstanceTypes instanceType)
            : base(instanceType)
        {
        }
    }
}