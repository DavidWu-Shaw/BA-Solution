using System;

namespace CRM.Core
{
    [Flags]
    public enum QuoteStatuses
    {
        // Must match with tblEntityItem.Value
        // select tblEntityItem.Value, tblEntityItem.Text, Code
        // from dbo.tblEntity inner join dbo.tblEntityItem on tblEntityItem.EntityId = tblEntity.EntityId
        // where tblEntity.Code = 'QuoteStatus'

        None = 0,
        Open = 1,
        Quoting = 2, 
        Approved = 3,
        Rejected = 4,
        Quoted = 5,
        Requote = 6,
    }
}
