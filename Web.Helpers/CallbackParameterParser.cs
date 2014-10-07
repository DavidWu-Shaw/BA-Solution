using System;
using System.Collections.Generic;

namespace Web.Helpers
{
    /// <summary>
    /// Library of methods for parse the callback string parameter
    /// </summary>
    public class CallbackParameterParser
    {
        /// <summary>
        /// To use this function the action and parameter must be separated by the char 179 (│) ( alt + 179 to insert );
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callbackParameter"></param>
        /// <returns></returns>
        public static T? ParseAction<T>(string callbackParameter) where T : struct
        {
            string[] actionParameters;
            return ParseAction<T>(callbackParameter, out actionParameters);
        }

        /// <summary>
        /// To use this function the action and parameter must be separated by the char 179 (│) ( alt + 179 to insert );
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callbackParameter"></param>
        /// <param name="actionParameters"></param>
        /// <returns></returns>
        public static T? ParseAction<T>(string callbackParameter, out string[] actionParameters) where T : struct
        {
            return ParseAction<T>(callbackParameter, out actionParameters, '│');
        }

        /// <summary>
        /// Split the string with the "separator" and parse the first string to the enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callbackParameter"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static T? ParseAction<T>(string callbackParameter, char separator) where T : struct
        {
            string[] actionParameters;
            return ParseAction<T>(callbackParameter, out actionParameters, separator);
        }

        /// <summary>
        /// Split the string with the "separator" and parse the first string to the enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="callbackParameter"></param>
        /// <param name="actionParameters"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static T? ParseAction<T>(string callbackParameter, out string[] actionParameters, char separator) where T : struct
        {
            actionParameters = new string[0];
            T? result = null;
            if (callbackParameter.HasValue())
            {
                foreach (T action in Enum.GetValues(typeof(T)))
                {
                    if (callbackParameter.StartsWith(action.ToString()))
                    {
                        result = action;
                        List<string> parameters = new List<string>(callbackParameter.Split(separator));
                        if (parameters.Count > 1)
                        {
                            parameters.RemoveAt(0);
                            actionParameters = parameters.ToArray();
                        }
                        break;
                    }
                }
            }
            return result;
        }
    }
}
