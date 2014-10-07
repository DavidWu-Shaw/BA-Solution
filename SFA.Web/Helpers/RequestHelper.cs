using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SFA.Web.Common;

namespace SFA.Web.Helpers
{
    public static class RequestHelper
    {
        public static string GetDomainUrl()
        {
            string protocol = "http";

            if (HttpContext.Current.Request.IsSecureConnection)
            {
                protocol += "s";
            }

            string host = GetHost();

            return string.Format("{0}://{1}", protocol, host);
        }

        public static string GetHost()
        {
            string host = HttpContext.Current.Request.Url.Host;

            if (!HttpContext.Current.Request.Url.IsDefaultPort)
            {
                host += ":" + HttpContext.Current.Request.Url.Port;
            }

            return host;
        }

        public static string GetRegularDomainUrl()
        {
            string host = GetHost();

            return string.Format("{0}://{1}", "http", host);
        }

        public static string GetSecureDomainUrl()
        {
            HttpRequest request = HttpContext.Current.Request;

            string host = GetHost();

            return string.Format("{0}://{1}", "https", host);
        }

        public static string GetAbsoluteRoot()
        {
            HttpRequest request = HttpContext.Current.Request;

            string appPath = HttpRuntime.AppDomainAppVirtualPath;

            if (!appPath.EndsWith("/"))
            {
                appPath += "/";
            }

            return string.Format("{0}{1}", GetDomainUrl(), appPath);
        }
    }
}