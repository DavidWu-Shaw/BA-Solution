
namespace Setup.Core
{
    public enum UserDomains
    {
        // Must match with core.tblDomain
        // select * from core.tblDomain

        //  1	SysAdmin
        //  2	Employee
        //  3	Customer
        //  4	Supplier
        //  5	SupplierAdmin
        //  6	Agent
        //  7	Anonymous

        SuperAdmin = 99,        // system defined
        SysAdmin = 1,
        Employee = 2,
        Customer = 3,
        Supplier = 4,
        SupplierAdmin = 5,
        Agent = 6,
        Anonymous = 7,
        Member = 8,
    }
}
