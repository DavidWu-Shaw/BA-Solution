
namespace CRM.Core
{
    public enum EntityTypes
    {
        // Must match with tblEntity
        // select EntityId, EntityKey
        // from dbo.tblEntity

        //  1	EventReccuringType
        //  2	EventObjectType
        //  3	CustomerType
        //  4	ContactMethodType
        //  5	ActivityType
        //  6	AddressType
        //  7	ProductCategory
        //  8	Currency
        //  9	ContactCategory
        //  10	UserDomain
        //  11	DocumentType
        //  12	OrderStatus
        //  13	Country
        //  14	SupplierCategory
        //  15	Nationality

        EventReccuringType = 1,
        EventObjectType = 2,
        CustomerType = 3,
        ContactMethodType = 4,
        ActivityType = 5,
        AddressType = 6,
        ProductCategory = 7,
        Currency = 8,
        ContactCategory = 9,
        UserDomain = 10,
        DocumentType = 11,
        OrderStatus = 12,
        Country = 13,
        SupplierCategory = 14,
        Nationality = 15,
        ReviewObjectType = 16,
    }
}
