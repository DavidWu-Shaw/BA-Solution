using System;

namespace General.Utility.DataValidate
{
    public enum ValidationItemType
    {
        Error = 1,
        Warning = 2
    }

    [Serializable]
    public class ValidationItem
    {
        public ValidationItemType ItemType { get; private set; }
        public string Message { get; private set; }

        public ValidationItem(string message)
        {
            ItemType = ValidationItemType.Error;
            Message = message;
        }

        public ValidationItem(ValidationItemType itemType, string message)
        {
            ItemType = itemType;
            Message = message;
        }
    }
}
