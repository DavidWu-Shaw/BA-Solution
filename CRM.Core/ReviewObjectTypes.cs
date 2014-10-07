
namespace CRM.Core
{
    public enum ReviewObjectTypes
    {
        // Must match with tblEntityItem.Value
        // select tblEntityItem.Value, tblEntityItem.Text, Code
        // from dbo.tblEntity inner join dbo.tblEntityItem on tblEntityItem.EntityId = tblEntity.EntityId
        // where tblEntity.Code = 'ReviewObjectType'

        // 1	Product	    ReviewObjectType
        // 2	Supplier	ReviewObjectType

        Product = 1,
        Supplier = 2,
        News = 3,
    }
}
