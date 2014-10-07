using System;

namespace General.Utility.DataValidate
{
    [Serializable]
    public class NotEmptyDataValidator : DataValidator, IDataValidator
    {
        private const string Message = "Empty";

        public DataValidateResult Validate(string value)
        {
            DataValidateResult result = new DataValidateResult();

            if (value == null || value.Trim().Length == 0)
            {
                result.AddItem(Message);
            }

            return result;
        }
    }
}
