using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Activation;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.WebServices
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProductService
    {
        [OperationContract]
        public IEnumerable<ProductInfoDto> RetrieveProductsBySupplier(int supplierId)
        {
            object languageId = WebContext.Current.ApplicationOption.DefaultLanguageId;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                var instances = facade.RetrieveProductsBySupplier(supplierId, new ProductInfoConverter(languageId));
                return instances;
            }
        }

        [OperationContract]
        public IEnumerable<SupplierInfoDto> RetrieveAllSupplier()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                var instances = facade.RetrieveAllSupplier(new SupplierInfoConverter());
                return instances;
            }
        }
    }
}
