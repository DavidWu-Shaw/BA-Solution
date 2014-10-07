using Microsoft.Practices.Unity;
using Framework.Unity;
using CRM.Service;
using CRM.Service.Contract;

namespace CRM.Configuration
{
    public class CRMServiceRegistry : IUnityRegistry
    {
        public void Initialize(IUnityContainer container)
        {
            container.RegisterType<IEmployeeService, EmployeeService>();
            container.RegisterType<ICustomerService, CustomerService>();
            container.RegisterType<IContactService, ContactService>();
            container.RegisterType<IActivityService, ActivityService>();
            container.RegisterType<IDocumentService, DocumentService>();
            container.RegisterType<IScheduleEventService, ScheduleEventService>();
            container.RegisterType<ISupplierService, SupplierService>();
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<ICatalogService, CatalogService>();
            container.RegisterType<ICategoryService, CategoryService>();
            container.RegisterType<IOpportunityService, OpportunityService>();
            container.RegisterType<ILeadService, LeadService>();
            container.RegisterType<IOrderService, OrderService>();
            container.RegisterType<IQuoteService, QuoteService>();
            container.RegisterType<ITransactionService, TransactionService>();
            container.RegisterType<IShipToService, ShipToService>();
            container.RegisterType<ISellingPeriodService, SellingPeriodService>();

            container.RegisterType<INewsService, NewsService>();
            container.RegisterType<ITopicService, TopicService>();
            container.RegisterType<IPostService, PostService>();
            container.RegisterType<IReviewService, ReviewService>();
        }
    }
}
