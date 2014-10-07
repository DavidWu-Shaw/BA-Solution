using System;
using System.Text;

namespace Web.Helpers
{
    public class JavaScriptMethod
    {
        private const string _methodDefinition = " function {0} ({1}) ";
        private string _methodName;
        private string[] _methodParameters;
        private StringBuilder _body = new StringBuilder();

        /// <summary>
        /// Initialize an new instance a JavaScript Method
        /// </summary>
        /// <param name="methodName"></param>
        /// <param name="methodParameters"></param>
        public JavaScriptMethod(string methodName, params string[] methodParameters)
            : this(null, methodName, methodParameters)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="methodName"></param>
        /// <param name="methodParameters"></param>
        public JavaScriptMethod(JavaScriptBlock owner, string methodName, params string[] methodParameters)
        {
            if (owner != null)
            {
                owner.Methods.Add(this);
            }
            _methodName = methodName;
            _methodParameters = methodParameters;
        }
        /// <summary>
        /// Add a line of code
        /// </summary>
        /// <param name="line"></param>
        public void AddCode(string line)
        {
            JavaScriptHelper.AppendLineToBuilderWithSpaces(_body, line);
        }

        /// <summary>
        /// Generate the method
        /// </summary>
        /// <returns></returns>
        public string GenerateCode()
        {
            StringBuilder completeMethod = new StringBuilder();
            string parameters = _methodParameters.BuildSeparatedList(", ");
            string methodDefinition = string.Format(_methodDefinition, _methodName, parameters);

            JavaScriptHelper.AppendLineToBuilderWithSpaces(completeMethod, methodDefinition);
            JavaScriptHelper.AppendLineToBuilderWithSpaces(completeMethod, "{");
            JavaScriptHelper.AppendLineToBuilderWithSpaces(completeMethod, _body.ToString());
            JavaScriptHelper.AppendLineToBuilderWithSpaces(completeMethod, "}");

            return completeMethod.ToString();
        }
    }
}
