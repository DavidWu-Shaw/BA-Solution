using System;
using System.Collections.Generic;
using System.Text;

namespace General.Utility.DataValidate
{
    [Serializable]
    public class CharSetValidator : DataValidator, IDataValidator
    {
        private IList<char> CharSet { get; set; }
        private string Message { get; set; }

        public CharSetValidator(IList<char> chars)
        {
            CharSet = chars;
            BuildMessage();
        }

        public DataValidateResult Validate(string value)
        {
            DataValidateResult result = new DataValidateResult();

            if (value != null && value.ToString().Trim().Length > 0)
            {
                try
                {
                    Char c = Convert.ToChar(value);
                    if (!CharSet.Contains(c))
                    {
                        result.AddItem(Message);
                    }
                }
                catch
                {
                    result.AddItem(Message);
                }
            }

            return result;
        }

        private void BuildMessage()
        {
            StringBuilder strChars = new StringBuilder();
            foreach (char c in CharSet)
            {
                strChars.Append(c);
                strChars.Append(",");
            }
            if (strChars.Length > 0)
            {
                strChars.Remove(strChars.Length - 1, 1);
            }

            Message = string.Format("Not in ({0})", strChars);
        }
    }
}
