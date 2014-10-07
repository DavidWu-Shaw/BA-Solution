
namespace CRM.Core
{
    public enum EventReccuringTypes
    {
        // Must match with tblEntityItem.Value
        // select tblEntityItem.Value, tblEntityItem.Text, Code
        // from dbo.tblEntity inner join dbo.tblEntityItem on tblEntityItem.EntityId = tblEntity.EntityId
        // where tblEntity.Code = 'EventReccuringType'

        // 1	Yearly	EventReccuringType
        // 2	Monthly	EventReccuringType
        // 3	Weekly	EventReccuringType
        // 4	Daily	EventReccuringType
        // 5	Once	EventReccuringType

        Yearly = 1,
        Monthly = 2,
        Weekly = 3,
        Daily = 4,
        Once = 5,
    }
}
