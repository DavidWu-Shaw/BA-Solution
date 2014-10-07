using System.Web.Mvc;
using Framework.Globalization;

namespace SFA.Web.Helpers
{
    public static class LocalizationHelper
    {
        public static string LocalizeController<T>(string key)
            where T : ControllerBase
        {
            string controllerName = typeof(T).Name;
            controllerName = controllerName.Remove(controllerName.Length - 10);
            string fullKey = string.Format("C_{0}_{1}", controllerName, key);

            return StringLocalizer.Localize(fullKey);
        }

        public static string LocalizeModel<T>(string key)
            where T : class
        {
            string modelName = typeof(T).Name;
            modelName = modelName.Remove(modelName.Length - 9);
            string fullKey = string.Format("M_{0}_{1}", modelName, key);

            return StringLocalizer.Localize(fullKey);
        }

        public static string LocalizeBusiness(string key)
        {
            string fullKey = string.Format("B_{0}", key);

            return StringLocalizer.Localize(fullKey);
        }

        public static string LocalizeEnum<T>(T value)
            where T : struct
        {
            string enumName = typeof(T).Name;
            string fullKey = string.Format("E_{0}_{1}", enumName, value.ToString());

            return StringLocalizer.Localize(fullKey);
        }

        public static string LocalizeView(string key)
        {
            string fullKey = string.Format("V_{0}", key);

            return StringLocalizer.Localize(fullKey);
        }

        public static string LocalizeView(string key, object languageId, string defaultValue)
        {
            string fullKey = string.Format("V_{0}", key);

            return StringLocalizer.Localize(fullKey, languageId, defaultValue);
        }
    }
}