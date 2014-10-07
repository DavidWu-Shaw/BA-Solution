using System.Web.Mvc;
using BA.UnityRegistry;
using CRM.ShopComponent;
using CRM.ShopComponent.Dto;
using Framework.UoW;
using SFA.Web.Models;
using SFA.Web.Models.Converters;

namespace SFA.Web.Controllers
{
    public class CartController : BaseController
    {
        public const string ControllerName = "Cart";
        public const string IndexAction = "Index";
        public const string SummaryAction = "Summary";
        public const string AddToCartAction = "AddToCart";
        public const string RemoveFromCartAction = "RemoveFromCart";
        public const string CheckoutAction = "Checkout";

        // GET: /Cart/
        public ActionResult Index(CartDto cart)
        {
            return View(cart);
        }

        public ViewResult AddToCart(CartDto cart, int productId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                UserCartFacade facade = new UserCartFacade(uow);
                facade.AddToCart(cart, productId, new ProductToCartItemConverter(CurrentLanguageId));
            }

            return View("Summary", cart);
        }

        public RedirectToRouteResult RemoveFromCart(CartDto cart, int productId)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                UserCartFacade facade = new UserCartFacade(uow);
                facade.RemoveFromCart(cart, productId);
            }

            return RedirectToAction(IndexAction);
        }

        public ViewResult Summary(CartDto cart)
        {
            return View(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingInfo());
        }

        [HttpPost]
        public ViewResult Checkout(CartDto cart, ShippingInfo shipInfo)
        {
            if (cart.QtyOrderedTotal == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty.");
            }
            if (ModelState.IsValid)
            {
                cart.Clear();
                return View("CheckoutCompleted");
            }
            else
            {
                return View(shipInfo);
            }
        }
    }
}
