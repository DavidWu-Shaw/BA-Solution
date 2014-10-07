using Framework.Data;

namespace Setup.Data
{
    public class LanguageData : DataObject
    {
        public LanguageData()
        {
        }

        public virtual string Name { get; set; }
        public virtual string Label { get; set; }
        public virtual string Culture { get; set; }
        public virtual bool IsPublished { get; set; }
    }
}
