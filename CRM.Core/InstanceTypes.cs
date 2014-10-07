
namespace CRM.Core
{
    public enum InstanceTypes
    {
        // These values must exactly match tblSubject->SubjectType
        UserMatch,      // This is special item for matching user 
        Customer,
        Contact,
        ContactContactMethod,
        ScheduleEvent,
        Activity,
        Product,
        Order,
        OrderItem,
        Opportunity,
        Lead,
        Supplier,
        Document,
        User,
        Language,
        Transaction,
        TransactionItem,
        ShipTo,
        SellingPeriod,
        Employee,
        MainMenu,
        SetupMenu,
        Domain,
        DomainMainMenu,
        DomainSetupMenu,
        News,
        Topic,
        Post,
        Review,
        Catalog,
        Quote,
        QuoteLine,
    }
}
