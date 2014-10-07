using System;

namespace General.Utility.DataValidate
{
    [Serializable]
    public class DecimalDataValidator : DataValidator, IDataValidator
    {
        private const string Message = "Non-Decimal";

        public DataValidateResult Validate(string value)
        {
            DataValidateResult result = new DataValidateResult();

            if (value != null && value.Trim().Length > 0)
            {
                try
                {
                    decimal v = Convert.ToDecimal(value);
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
