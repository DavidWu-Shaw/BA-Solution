
namespace BA.Web.Common
{
    public class AnonymousBasePage : BasePage
    {
        public AnonymousBasePage()
        {
            AllowAnonymous = true;
            IsNarrowPage = true;
        }
    }
}