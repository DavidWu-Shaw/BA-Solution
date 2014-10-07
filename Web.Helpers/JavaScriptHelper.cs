using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI;

namespace Web.Helpers
{
    public class JavaScriptHelper
    {
        private static Dictionary<string, string> _jsClientSideVariables;
        private static Dictionary<string, string> _jsClientScripts;
        private static Dictionary<string, string> _jsClientScriptsFile;

        /// <summary>
        /// List of Client side variables
        /// </summary>
        public static Dictionary<string, string> JsClientSideVariables
        {
            get
            {
                if (_jsClientSideVariables == null)
                {
                    _jsClientSideVariables = new Dictionary<string, string>();
                }

                return _jsClientSideVariables;
            }
        }

        /// <summary>
        /// List of client script
        /// </summary>
        public static Dictionary<string, string> JsClientScripts
        {
            get
            {
                if (_jsClientScripts == null)
                {
                    _jsClientScripts = new Dictionary<string, string>();
                }

                return _jsClientScripts;
            }
        }

        /// <summary>
        /// List of Script file
        /// </summary>
        public static Dictionary<string, string> JsClientScriptsFile
        {
            get
            {
                if (_jsClientScriptsFile == null)
                {
                    _jsClientScriptsFile = new Dictionary<string, string>();
                }

                return _jsClientScriptsFile;
            }
        }


        /// <summary>
        /// Append Line To Builder With Spaces
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="line"></param>
        public static void AppendLineToBuilderWithSpaces(StringBuilder builder, string line)
        {
            builder.Append(" ");
            builder.Append(line);
            builder.Append(" ");
            builder.Append(Environment.NewLine);
        }

        /// <summary>
        /// Build Js Client Side Variables
        /// </summary>
        /// <param name="jsVars"></param>
        /// <returns></returns>
        public static string BuildJsClientSideVariables(Dictionary<string, string> jsVars)
        {
            StringBuilder jsScript = new StringBuilder();

            if (jsVars != null && jsVars.Count > 0)
            {
                foreach (KeyValuePair<string, string> variable in jsVars)
                {
                    jsScript.Append("var " + variable.Key + " = \"" + variable.Value + "\";\r\n");
                }
            }

            return jsScript.ToString();
        }

        /// <summary>
        /// Load Js Client Scripts
        /// </summary>
        /// <param name="page"></param>
        /// <param name="jsClientScriptsFile"></param>
        /// <param name="jsClientScripts"></param>
        /// <param name="jsClientSideVariables"></param>
        public static void LoadJsClientScripts(Page page, Dictionary<string, string> jsClientScriptsFile, Dictionary<string, string> jsClientScripts, Dictionary<string, string> jsClientSideVariables)
        {
            StringBuilder jsScript = new StringBuilder();

            HtmlGenericControl scriptControl = new HtmlGenericControl("script");
            scriptControl.Attributes.Add("type", "text/javascript");

            // Add JS Variables
            string jsVars = JavaScriptHelper.BuildJsClientSideVariables(jsClientSideVariables);

            jsScript.Append(jsVars);

            // Add JS Scripts
            foreach (KeyValuePair<string, string> script in jsClientScripts)
            {
                jsScript.Append(script.Value);
            }

            // Load to Browser
            scriptControl.InnerHtml = jsScript.ToString();
            page.Header.Controls.Add(scriptControl);

            foreach (KeyValuePair<string, string> jsFile in jsClientScriptsFile)
            {
                scriptControl = new HtmlGenericControl("script");
                scriptControl.Attributes.Add("type", "text/javascript");
                scriptControl.Attributes.Add("src", page.Request.ApplicationPath + jsFile.Value);
                page.Header.Controls.Add(scriptControl);
            }

            JavaScriptHelper.ClearLists();
        }

        /// <summary>
        /// Clear all List
        /// </summary>
        public static void ClearLists()
        {
            JsClientScripts.Clear();
            JsClientScriptsFile.Clear();
            JsClientSideVariables.Clear();
        }

        /// <summary>
        /// Register a Script ressource
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        public static void RegisterScriptResource<T>(Page page)
        {
            Type controlType = typeof(T);
            string jsName = controlType.Name;
            string jsNamespace = controlType.Namespace;

            if (jsNamespace.EndsWith(".UserControls"))
            {
                jsNamespace = jsNamespace.Replace(".UserControls", "");
            }
            else if (jsNamespace.EndsWith(".Controls"))
            {
                jsNamespace = jsNamespace.Replace(".Controls", "");
            }

            jsNamespace += ".Javascripts";

            RegisterScriptResource(page, controlType, jsNamespace, jsName);
        }

        /// <summary>
        /// Register a Script ressource
        /// </summary>
        /// <param name="page"></param>
        /// <param name="controlType"></param>
        /// <param name="jsNamespace"></param>
        /// <param name="jsName"></param>
        public static void RegisterScriptResource(Page page, Type controlType, string jsNamespace, string jsName)
        {
            string jsFileName = jsName + ".js";
            WebResourceHelper.RegisterWebResource(page, controlType, jsNamespace, jsFileName);
        }
    }
}
