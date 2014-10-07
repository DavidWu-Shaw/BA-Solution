using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Converters;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.WebServices
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class CategoryWebService
    {
        [WebGet(UriTemplate = "RetrieveCategoryList")]
        public IEnumerable<CRM.Component.Dto.CategoryDto> RetrieveCategoryList()
        {
            object languageId = WebContext.Current.ApplicationOption.DefaultLanguageId;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CRM.Component.CategoryFacade facade = new CRM.Component.CategoryFacade(uow);
                var instances = facade.RetrieveCategoryTree(WebContext.Current.ApplicationOption.GlobalProductCatalogId, new CategoryConverter(languageId));
                return instances;
            }
        }

        [WebGet(UriTemplate = "RetrieveProducts/{categoryId}")]
        public IEnumerable<ProductInfoDto> RetrieveProducts(string categoryId)
        {
            int id = Convert.ToInt32(categoryId);
            object languageId = WebContext.Current.ApplicationOption.DefaultLanguageId;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                var instances = facade.RetrieveProductsByCategory(categoryId, new ProductInfoConverter(languageId));
                return instances;
            }
        }

        [WebGet(UriTemplate = "RetrieveProductsBySupplier/{supplierId}")]
        public IEnumerable<ProductInfoDto> RetrieveProductsBySupplier(string supplierId)
        {
            int id = Convert.ToInt32(supplierId);
            object languageId = WebContext.Current.ApplicationOption.DefaultLanguageId;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                var instances = facade.RetrieveProductsBySupplier(id, new ProductInfoConverter(languageId));
                return instances;
            }
        }

        [WebGet(UriTemplate = "RetrieveAllSupplier")]
        public IEnumerable<SupplierInfoDto> RetrieveAllSupplier()
        {
            object languageId = WebContext.Current.ApplicationOption.DefaultLanguageId;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                var instances = facade.RetrieveAllSupplier(new SupplierInfoConverter(languageId));
                return instances;
            }
        }
    }
}