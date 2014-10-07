
namespace BA.Web.Common
{
    public class AdminSetupBasePage : SetupBasePage
    {
        protected override bool CheckPagePermission()
        {
            return CurrentUserContext.IsSuperAdmin;
        }
    }
}