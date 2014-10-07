using System;
using System.Web.UI;

namespace Web.Helpers
{
    /// <summary>
    /// Web resource helper
    /// </summary>
    public class WebResourceHelper
    {
        /// <summary>
        /// Register a web ressource
        /// Don't forget the WebResource Attribute
        /// </summary>
        /// <param name="page"></param>
        /// <param name="controlType"></param>
        /// <param name="fileNamespace"></param>
        /// <param name="fileName"></param>
        public static void RegisterWebResource(Page page, Type controlType, string fileNamespace, string fileName)
        {
            string resourceUrl = page.ClientScript.GetWebResourceUrl(controlType, fileNamespace + "." + fileName);
            page.ClientScript.RegisterClientScriptInclude(fileName, resourceUrl);
        }

        /// <summary>
        /// Get the image resource URL
        /// </summary>
        /// <typeparam name="ControlType"></typeparam>
        /// <param name="page"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetImageUrl<ControlType>(Page page, string fileName)
        {
            Type controlType = typeof(ControlType);
            string imgNamespace = "SimpleDesk.WebControls.Images";

            return page.ClientScript.GetWebResourceUrl(controlType, imgNamespace + "." + fileName);
        }
    }
}
