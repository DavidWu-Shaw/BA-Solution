using System.Collections.Generic;
using System.Text;

namespace Web.Helpers
{
    public class JavaScriptBlock
    {

        public const string StartTag = "<script type=\"text/javascript\">// <!CDATA[";
        public const string EndTag = "// ]]></script>";
        private List<JavaScriptMethod> _methods = new List<JavaScriptMethod>();
        private string _blockName;

        /// <summary>
        /// Initialize a new instance of the JavaScriptBlock
        /// </summary>
        /// <param name="blockName"></param>
        public JavaScriptBlock(string blockName)
        {
            _blockName = blockName;
        }

        /// <summary>
        /// List of the Javascript Method
        /// </summary>
        /// <see cref="JavaScriptMethod"/>
        public List<JavaScriptMethod> Methods
        {
            get { return _methods; }
        }


        /// <summary>
        /// Generate the code of the block
        /// </summary>
        /// <returns></returns>
        public string GenerateCode()
        {
            StringBuilder completeSection = new StringBuilder();



            JavaScriptHelper.AppendLineToBuilderWithSpaces(completeSection, StartTag);

            foreach (JavaScriptMethod method in Methods)
            {
                JavaScriptHelper.AppendLineToBuilderWithSpaces(completeSection, method.GenerateCode());
            }

            JavaScriptHelper.AppendLineToBuilderWithSpaces(completeSection, EndTag);

            return completeSection.ToString();
        }

    }
}
