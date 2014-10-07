
namespace BA.Web.Common
{
    public class AppBasePage : BasePage
    {
        public AppBasePage()
        {
            AllowAnonymous = false;
            IsNarrowPage = false;
            RequireSSL = true;
        }

        protected override bool CheckPagePermission()
        {
            return (CurrentUserContext.IsAdmin || CurrentUserContext.IsEmployee);
        }
    }
}