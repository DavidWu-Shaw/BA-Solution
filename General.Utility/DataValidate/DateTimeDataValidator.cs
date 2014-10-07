using System;

namespace General.Utility.DataValidate
{
    [Serializable]
    public class DateTimeDataValidator : DataValidator, IDataValidator
    {
        private const string Message = "Non-Date";

        public DataValidateResult Validate(string value)
        {
            DataValidateResult result = new DataValidateResult();
            if (value != null && value.Trim().Length > 0 && value != "0")
            {
                try
                {
                    Convert.ToDateTime(value);
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
