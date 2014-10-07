using System;

namespace CRM.Core
{
    [Flags]
    public enum OrderStatuses
    {
        // Must match with tblEntityItem.Value
        // select tblEntityItem.Value, tblEntityItem.Text, Code
        // from dbo.tblEntity inner join dbo.tblEntityItem on tblEntityItem.EntityId = tblEntity.EntityId
        // where tblEntity.Code = 'Order Status'

        // 1	Accepted	Order Status
        // 2	Delivering	Order Status
        // 4	Open	Order Status
        // 8	Completed	Order Status
        // 16	Incomplete	Order Status
        // 32	Cancelled	Order Status
        // 64	Rejected	Order Status

        None = 0,
        Open = 4,
        Accepted = 1,
        Delivering = 2,
        Completed = 8,
        Incomplete = 16,
        Cancelled = 32,
        Rejected = 64,
    }
}
