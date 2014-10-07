using Framework.Unity;
using Microsoft.Practices.Unity;
using CRM.Repository.Contract;
using CRM.Repository;

namespace BA.UnityRegistry
{
    public class CRMRepositoryRegistry : IUnityRegistry
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IEmployeeRepository, EmployeeRepository>();
            container.RegisterType<ICustomerRepository, CustomerRepository>();
            container.RegisterType<IContactRepository, ContactRepository>();
            container.RegisterType<IActivityRepository, ActivityRepository>();
            container.RegisterType<IDocumentRepository, DocumentRepository>();
            container.RegisterType<IScheduleEventRepository, ScheduleEventRepository>();
            container.RegisterType<ISupplierRepository, SupplierRepository>();
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<ICatalogRepository, CatalogRepository>();
            container.RegisterType<ICategoryRepository, CategoryRepository>();
            container.RegisterType<IOpportunityRepository, OpportunityRepository>();
            container.RegisterType<ILeadRepository, LeadRepository>();
            container.RegisterType<IOrderRepository, OrderRepository>();
            container.RegisterType<IQuoteRepository, QuoteRepository>();
            container.RegisterType<ITransactionRepository, TransactionRepository>();
            container.RegisterType<IShipToRepository, ShipToRepository>();
            container.RegisterType<ISellingPeriodRepository, SellingPeriodRepository>();

            container.RegisterType<INewsRepository, NewsRepository>();
            container.RegisterType<ITopicRepository, TopicRepository>();
            container.RegisterType<IPostRepository, PostRepository>();
            container.RegisterType<IReviewRepository, ReviewRepository>();
        }
    }
}
