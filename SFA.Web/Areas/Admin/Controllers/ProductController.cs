using System.Collections.Generic;
using System.Web.Mvc;
using BA.UnityRegistry;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Core;
using CRM.Data;
using Framework.Component;
using Framework.UoW;
using SFA.Web.Areas.Admin.Controllers.Converters;
using SFA.Web.Areas.Admin.Models;

namespace SFA.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : InstanceController
    {
        public const string ControllerName = "Product";
        public const string IndexAction = "Index";

        public ProductController()
        {
            InstanceType = InstanceTypes.Product;
        }

        //
        // GET: /Admin/Product/

        public ActionResult Index()
        {
            ListViewModel model = new ListViewModel(InstanceType);
            model.Instances = GetProductList();
            return ListView(model);
        }

        //
        // GET: /Admin/Product/Details/5

        public ActionResult Detail(int id)
        {
            InstanceViewModel model = new InstanceViewModel(InstanceType);
            model.Instance = GetProduct(id);
            return DetailView(model);
        }

        //
        // GET: /Admin/Product/Edit/5

        public ActionResult Edit(int id)
        {
            InstanceViewModel model = new InstanceViewModel(InstanceType);
            model.Instance = GetProduct(id);
            return EditView(model);
        }

        //
        // POST: /Admin/Product/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, ProductDto instance)
        {
            InstanceViewModel model = new InstanceViewModel(InstanceType);
            if (ModelState.IsValid)
            {
                instance.Id = id;
                IFacadeUpdateResult<ProductData> result = SaveProduct(instance);

                if (result.IsSuccessful)
                {
                    ProductDto savedInstance = result.ToDto<ProductDto>(new ProductConverter());
                    model.Instance = savedInstance;
                    return EditView(model);
                }
                else
                {
                    // Deal with Update failed result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
            model.Instance = instance;
            return EditView(model);
        }

        //
        // GET: /Admin/Product/Delete/5

        public ActionResult Delete(int id)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                IFacadeUpdateResult<ProductData> result = facade.DeleteProduct(id);
                if (result.IsSuccessful)
                {
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }

            return RedirectToAction(IndexAction);
        }

        private IEnumerable<ProductDto> GetProductList()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade productFacade = new ProductFacade(uow);
                return productFacade.RetrieveAllProduct(new ProductConverter());
            }
        }

        private ProductDto GetProduct(int id)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                return facade.RetrieveOrNewProduct(id, new ProductConverter());
            }
        }

        private IFacadeUpdateResult<ProductData> SaveProduct(ProductDto instance)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                IFacadeUpdateResult<ProductData> result = facade.SaveProduct(instance);
                return result;
            }
        }
    }
}
