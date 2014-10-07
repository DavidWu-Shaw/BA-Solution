using System.Collections.Generic;
using System.Linq;
using System.Web.Services;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Shop.Converters;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;

namespace BA.Web.WebServices
{
    /// <summary>
    /// Summary description for ProductWebService
    /// </summary>
    [WebService(Namespace = "http://www.orderbyapps.com/WebServices/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ProductWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public List<ProductInfoDto> RetrieveProductsBySupplier(int supplierId)
        {
            object languageId = WebContext.Current.ApplicationOption.DefaultLanguageId;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                var instances = facade.RetrieveProductsBySupplier(supplierId, new ProductInfoConverter(languageId));

                return instances.ToList();
            }
        }

        [WebMethod]
        public List<SupplierInfoDto> RetrieveAllSupplier()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                var instances = facade.RetrieveAllSupplier(new SupplierInfoConverter());

                return instances.ToList();
            }
        }
    }
}
