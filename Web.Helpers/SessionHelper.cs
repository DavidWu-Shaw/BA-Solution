using System.Web;

namespace Web.Helpers
{
    public class SessionHelper
    {
        public static void SaveInSession(object obj, string sessionIndex)
        {
            HttpContext.Current.Session[sessionIndex] = obj;
        }

        public static object GetFromSession(string sessionIndex)
        {
            return HttpContext.Current.Session[sessionIndex];
        }

        public static void RemoveFromSession(string sessionIndex)
        {
            if (IsInSession(sessionIndex))
            {
                HttpContext.Current.Session.Remove(sessionIndex);
            }
        }

        public static bool IsInSession(string sessionIndex)
        {
            if (HttpContext.Current.Session != null && HttpContext.Current.Session[sessionIndex] != null)
            {
                return true;
            }

            return false;
        }
    }
}