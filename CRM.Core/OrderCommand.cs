
namespace CRM.Core
{
    public enum OrderCommand
    {
        //Open = 4,
        //Accepted = 1,
        //Delivering = 2,
        //Completed = 8,
        //Incomplete = 16,
        //Cancelled = 32,
        //Rejected = 64,

        // The hashcodes must match OrderStatus values
        Accept = 1,
        Delivering = 2,
        Complete = 8,
        Incomplete = 16,
        Cancel = 32,
        Reject = 64,
    }
}
