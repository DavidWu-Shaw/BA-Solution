using System;
using System.Collections.Generic;

namespace General.Utility.DataValidate
{
    [Serializable]
    public class DataValidateResult
    {
        public IList<ValidationItem> Items
        {
            get;
            private set; 
        }

        public bool IsValid
        {
            get
            {
                return Items.Count == 0;
            }
        }

        public DataValidateResult()
        {
            Items = new List<ValidationItem>();
        }

        public void AddItem(string message)
        {
            ValidationItem item = new ValidationItem(message);
            Items.Add(item);
        }

        public void Merge(DataValidateResult result)
        {
            foreach (ValidationItem item in result.Items)
            {
                Items.Add(item);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
