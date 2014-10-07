using Framework.Data;

namespace SubjectEngine.Data
{
	public class SubjectFieldLanguageData : ChildDataObject
	{
		public virtual object LanguageId { get; set; }
		public virtual string FieldLabel { get; set; } 
		public virtual string HelpText { get; set; }
	}
}
