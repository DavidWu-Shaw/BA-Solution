
namespace BA.Web.Common
{
    public class SetupBasePage : BasePage
    {
        public SetupBasePage()
        {
            AllowAnonymous = false;
            IsNarrowPage = false;
        }

        protected override bool CheckPagePermission()
        {
            return true;
        }
    }
}