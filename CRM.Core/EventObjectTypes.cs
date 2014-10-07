
namespace CRM.Core
{
    public enum EventObjectTypes
    {
        // Must match with tblEntityItem.Value
        // select tblEntityItem.Value, tblEntityItem.Text, Code
        // from dbo.tblEntity inner join dbo.tblEntityItem on tblEntityItem.EntityId = tblEntity.EntityId
        // where tblEntity.Code = 'EventObjectType'

        // 1	Contact	    EventObjectType
        // 2	Customer	EventObjectType
        // 3	User	    EventObjectType
        // 4	Supplier	EventObjectType
        // 5	Product	    EventObjectType
        // 6	Order	    EventObjectType

        Contact = 1,
        Customer = 2,
        Employee = 3, 
        User = 4,
        Supplier = 5,
        Product = 6,
        Order = 7,
    }
}
