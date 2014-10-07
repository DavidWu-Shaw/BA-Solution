using System;

namespace General.Utility.DataValidate
{
    [Serializable]
    public class FixLengthDataValidator : DataValidator, IDataValidator
    {
        public int Length { get; set; }

        public FixLengthDataValidator()
        {
        }

        public FixLengthDataValidator(int length)
        {
            Length = length;   
        }

        public DataValidateResult Validate(string value)
        {
            DataValidateResult result = new DataValidateResult();

            string message = string.Empty;
            if (value == null || value.Trim().Length < Length)
            {
                message = string.Format("Length < {0}", Length);
            }
            else if (value.Trim().Length > Length)
            {
                message = string.Format("Length > {0}", Length);
            }

            if (message.Length > 0)
            {
                result.AddItem(message);
            }

            return result;
        }
    }
}
