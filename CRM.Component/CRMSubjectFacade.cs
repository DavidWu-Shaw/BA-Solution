using System;
using System.Collections.Generic;
using Framework.Component;
using CRM.Core;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Core;
using Framework.UoW;
using Setup.Component;
using SubjectEngine.Component;
using SubjectEngine.Component.Dto;
using SubjectEngine.Core;

namespace CRM.Component
{
    public class CRMSubjectFacade : BusinessComponent
    {
        public CRMSubjectFacade(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void AttachProperties(SubjectDto subjectDto)
        {
            foreach (SubjectFieldDto field in subjectDto.SubjectFields)
            {
                // Retrieve Pickup type ListDataSource
                if (field.DucType == DucTypes.Pickup && field.PickupEntityId != null)
                {
                    DEntityFacade facade = new DEntityFacade(UnitOfWork);
                    field.ListDataSource = facade.GetEntityItemList(field.PickupEntityId);
                }
                // Retrieve Lookup type ListDataSource
                else if (field.DucType == DucTypes.Lookup && field.LookupSubjectId != null)
                {
                    field.ListDataSource = GetBindingList(field.LookupSubjectType);
                }
            }
        }

        private IList<BindingListItem> GetBindingList(string subjectType)
        {
            IList<BindingListItem> dataSource = null;

            switch ((InstanceTypes)Enum.Parse(typeof(InstanceTypes), subjectType))
            {
                case InstanceTypes.Customer:
                    CustomerSystem sys = new CustomerSystem(UnitOfWork);
                    dataSource = sys.GetBindingList();
                    break;
                case InstanceTypes.Contact:
                    ContactSystem contactSystem = new ContactSystem(UnitOfWork);
                    dataSource = contactSystem.GetBindingList();
                    break;
                case InstanceTypes.Product:
                    ProductSystem productSystem = new ProductSystem(UnitOfWork);
                    dataSource = productSystem.GetBindingList();
                    break;
                case InstanceTypes.Supplier:
                    SupplierSystem supplierSystem = new SupplierSystem(UnitOfWork);
                    dataSource = supplierSystem.GetBindingList();
                    break;
                case InstanceTypes.Catalog:
                    CatalogSystem catalogSystem = new CatalogSystem(UnitOfWork);
                    dataSource = catalogSystem.GetBindingList();
                    break;
                case InstanceTypes.Employee:
                    EmployeeSystem employeeSystem = new EmployeeSystem(UnitOfWork);
                    dataSource = employeeSystem.GetBindingList();
                    break;
                case InstanceTypes.Document:
                    DocumentSystem docSystem = new DocumentSystem(UnitOfWork);
                    dataSource = docSystem.GetBindingList();
                    break;
                case InstanceTypes.User:
                    UserFacade facade = new UserFacade(UnitOfWork);
                    dataSource = facade.GetBindingList();
                    break;
                case InstanceTypes.Language:
                    LanguageFacade languageFacade = new LanguageFacade(UnitOfWork);
                    dataSource = languageFacade.GetBindingList();
                    break;
                case InstanceTypes.Domain:
                    DomainFacade domainFacade = new DomainFacade(UnitOfWork);
                    dataSource = domainFacade.GetBindingList();
                    break;
                case InstanceTypes.MainMenu:
                    MainMenuFacade mainMenuFacade = new MainMenuFacade(UnitOfWork);
                    dataSource = mainMenuFacade.GetBindingList();
                    break;
                case InstanceTypes.SetupMenu:
                    SetupMenuFacade setupMenuFacade = new SetupMenuFacade(UnitOfWork);
                    dataSource = setupMenuFacade.GetBindingList();
                    break;
                case InstanceTypes.ShipTo:
                    ShipToFacade shipTofacade = new ShipToFacade(UnitOfWork);
                    dataSource = shipTofacade.GetBindingList();
                    break;
                case InstanceTypes.SellingPeriod:
                    SellingPeriodFacade sellingPeriodfacade = new SellingPeriodFacade(UnitOfWork);
                    dataSource = sellingPeriodfacade.GetBindingList();
                    break;
                case InstanceTypes.UserMatch:
                    dataSource = GetUserMatch();
                    break;
            }

            return dataSource;
        }

        private IList<BindingListItem> GetUserMatch()
        {
            IList<BindingListItem> dataSource = new List<BindingListItem>();

            ISupplierService service = UnitOfWork.GetService<ISupplierService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (SupplierData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, "Supplier: " + data.Name));
                }
            }

            IEmployeeService service2 = UnitOfWork.GetService<IEmployeeService>();
            var query2 = service2.GetAll();
            if (query2.HasResult)
            {
                foreach (EmployeeData data in query2.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, "Employee: " + data.Name));
                }
            }

            ICustomerService customerService = UnitOfWork.GetService<ICustomerService>();
            var customerQuery = customerService.GetAll();
            if (customerQuery.HasResult)
            {
                foreach (CustomerData data in customerQuery.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, "Customer: " + data.Name));
                }
            }

            return dataSource;
        }
    }
}
