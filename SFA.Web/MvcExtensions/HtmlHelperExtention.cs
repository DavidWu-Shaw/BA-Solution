using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SFA.Web.Helpers;
using SFA.Web.Controllers;
using SFA.Web.Common;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtention
    {
        public static string Localize(this HtmlHelper htmlHelper, string key)
        {
            return LocalizationHelper.LocalizeView(key);
        }

        public static string Localize(this HtmlHelper htmlHelper, string key, string defaultValue)
        {
            BaseController controller = htmlHelper.ViewContext.Controller as BaseController;
            return LocalizationHelper.LocalizeView(key, controller.CurrentLanguageId, defaultValue);
        }

        public static string LocalizeEnum<T>(this HtmlHelper htmlHelper, T value)
            where T : struct
        {
            return LocalizationHelper.LocalizeEnum<T>(value);
        }

        public static string LocalizeData(this HtmlHelper htmlHelper, string keyFormatString, object id, string defaultValue)
        {
            BaseController controller = htmlHelper.ViewContext.Controller as BaseController;
            return WebContext.GetLocalizedData(keyFormatString, id, controller.CurrentLanguageId, defaultValue);
        }

    }
}