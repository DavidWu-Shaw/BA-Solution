using System.Collections.Generic;
using System.Web.Mvc;
using BA.UnityRegistry;
using CRM.Component.Dto;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using SFA.Web.Common;
using SFA.Web.Helpers;
using SFA.Web.Models;
using SFA.Web.Models.Converters;

namespace SFA.Web.Controllers
{
    public class ProductController : BaseController
    {
        public const string ControllerName = "Product";
        public const string IndexAction = "Index";
        public const string DetailAction = "Detail";
        public const string ExplorerAction = "Explorer";
        public const string GetImageAction = "GetImage";
        public const string ProductListAction = "ProductList";
        public const string QuoteRequestAction = "QuoteRequest";
        public const string QuoteConfirmAction = "QuoteConfirm";

        // GET: /Product/
        public ActionResult Index()
        {
            return View("Explorer", GetExplorerModel(null));
        }

        public ActionResult Explorer(int id)
        {
            return View("Explorer", GetExplorerModel(id));
        }

        public PartialViewResult ProductList(int? id)
        {
            return PartialView(GetProductList(id));
        }

        public ActionResult Detail(int id)
        {
            return View(RetrieveProductInfo(id));
        }

        public ActionResult QuoteRequest(int id)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                QuoteFacade facade = new QuoteFacade(uow);
                QuoteInfoDto quote = facade.CreateQuote(id);
                return View(quote);
            }
        }

        [HttpPost]
        public ActionResult QuoteRequest(int id, QuoteInfoDto quote)
        {
            if (ModelState.IsValid)
            {
                return View("QuoteComplete", quote);
            }
            return View(quote);
        }

        [HttpPost]
        public ActionResult QuoteConfirm(QuoteInfoDto quote)
        {
            return PartialView(quote);
        }

        public FileContentResult GetImage(int id)
        {
            ProductInfoDto product = null;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade productFacade = new ProductFacade(uow);
                product = productFacade.RetrieveProductInfo(id, new ProductInfoConverter());
            }

            if (product != null && product.Sketch != null)
            {
                return File(product.Sketch, "image/jpg");
            }
            else
            {
                return null;
            }
        }

        private ProductExplorerViewModel GetExplorerModel(int? categoryId)
        {
            ProductExplorerViewModel model = new ProductExplorerViewModel();
            model.CurrentCategoryId = categoryId;

            IEnumerable<CategoryDto> categoryList = null;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                CRM.Component.CategoryFacade facade = new CRM.Component.CategoryFacade(uow);
                categoryList = facade.RetrieveCategoryTree(WebContext.Current.ApplicationOption.GlobalProductCatalogId, new CategoryConverter(CurrentLanguageId));
            }
            if (categoryList != null)
            {
                CategoryTreeBuilder treeBuilder = new CategoryTreeBuilder(categoryList);
                model.CategoryTree = treeBuilder.CategoryTree;
            }
            else
            {
                model.CategoryTree = new CategoryNode();
            }

            return model;
        }

        private IEnumerable<ProductInfoDto> GetProductList(int? categoryId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade productFacade = new ProductFacade(uow);
                if (categoryId.HasValue)
                {
                    return productFacade.RetrieveProductsByCategory(categoryId, new ProductInfoConverter(CurrentLanguageId));
                }
                else
                {
                    return productFacade.RetrieveGlobalProducts(WebContext.Current.ApplicationOption.GlobalProductCatalogId, new ProductInfoConverter(CurrentLanguageId));
                }
            }
        }

        private ProductInfoDto RetrieveProductInfo(object instanceId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                ProductInfoDto instance = facade.RetrieveProductInfo(instanceId, new ProductInfoConverter(CurrentLanguageId));
                // Get product reviews
                //ReviewFacade reviewFacade = new ReviewFacade(uow);
                //instance.Reviews = reviewFacade.RetrieveReviewsByProduct(instanceId, new ReviewInfoConverter());

                return instance;
            }
        }
    }
}
