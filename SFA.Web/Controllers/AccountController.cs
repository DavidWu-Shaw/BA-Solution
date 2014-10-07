using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BA.UnityRegistry;
using Framework.Security;
using Framework.UoW;
using Setup.Component;
using SFA.Web.Models;

namespace SFA.Web.Controllers
{
    //[RequireHttps]
    public class AccountController : BaseController
    {
        public const string ControllerName = "Account";
        public const string SignInAction = "SignIn";
        public const string SignOutAction = "SignOut";
        public const string RegisterAction = "Register";

        // GET: /Account/SignIn
        public ActionResult SignIn()
        {
            return View();
        }

        // POST: /Account/SignIn
        [HttpPost]
        public ActionResult SignIn(SignInViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                UserIdentity user = ProcAuthentication(model.Email, EncryptionHelper.Encrypt(model.Password));

                if (user != null)
                {
                    CurrentUserContext.Initilize(user);

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(user.Email, model.RememberMe, 24 * 60);

                    // Encrypt the ticket.
                    string encTicket = FormsAuthentication.Encrypt(ticket);

                    // Create the cookie.
                    System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));
                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/SignOut

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.RemoveAll();

            return RedirectToAction(HomeController.HomeAction, HomeController.ControllerName);
        }

        //
        // GET: /Account/Register

        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //// Attempt to register the user
                //MembershipCreateStatus createStatus;
                //Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, true, null, out createStatus);

                //if (createStatus == MembershipCreateStatus.Success)
                //{
                //    FormsAuthentication.SetAuthCookie(model.UserName, false /* createPersistentCookie */);
                //    return RedirectToAction("Index", "Home");
                //}
                //else
                //{
                //    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        // GET: /Account/ChangeUserProfile
        [Authorize]
        public ActionResult ChangeUserProfile()
        {
            return View();
        }

        // POST: /Account/ChangeUserInfo
        [Authorize]
        [HttpPost]
        public ActionResult ChangeUserInfo(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
            }
            return View(model);
        }

        // GET: /Account/ChangePassword
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //// ChangePassword will throw an exception rather
                //// than return false in certain failure scenarios.
                //bool changePasswordSucceeded;
                //try
                //{
                //    MembershipUser currentUser = Membership.GetUser(User.Identity.Name, true /* userIsOnline */);
                //    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword, model.NewPassword);
                //}
                //catch (Exception)
                //{
                //    changePasswordSucceeded = false;
                //}

                //if (changePasswordSucceeded)
                //{
                //    return RedirectToAction("ChangePasswordSuccess");
                //}
                //else
                //{
                //    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                //}
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ChangePasswordSuccess

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        private UserIdentity ProcAuthentication(string email, string encryptedPassword)
        {
            UserIdentity authenticatedUser = null;
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                AuthenticateFacade facade = new AuthenticateFacade(uow);
                authenticatedUser = facade.Authenticate(email, encryptedPassword);
            }

            return authenticatedUser;
        }

    }
}
