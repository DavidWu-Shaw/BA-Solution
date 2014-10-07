using Framework.Data;

namespace SubjectEngine.Data
{
	public class SubjectLanguageData : ChildDataObject
	{
		public virtual object LanguageId { get; set; }
		public virtual string SubjectLabel { get; set; }
		public virtual string Description { get; set; }
	}
}
