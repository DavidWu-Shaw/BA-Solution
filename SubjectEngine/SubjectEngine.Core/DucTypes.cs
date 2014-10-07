using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SubjectEngine.Core
{
    public enum DucTypes
    {
        // These values must exactly match tblDataType->DucType
        Text,
        Lookup,
        Pickup,
        Number,
        Currency,
        Date,
        Datetime,
        Time,
        Email,
        Phone,
        Checkbox,
        TextArea,
        Hierarchical,
        Formula,
        Percentage,
        AutoNumber,
        Memo,
        URL,
        Integer,
        Image,
        Attachment,
    }
}
