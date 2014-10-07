using System;

namespace General.Utility.DataValidate
{
    [Serializable]
    public class IntegerDataValidator : DataValidator, IDataValidator
    {
        private const string Message = "Non-Integer";

        public DataValidateResult Validate(string value)
        {
            DataValidateResult result = new DataValidateResult();

            if (value != null && value.Trim().Length > 0)
            {
                try
                {
                    int v = Convert.ToInt32(value);
                }
                catch
                {
                    result.AddItem(Message);
                }
            }

            return result;
        }
    }
}
