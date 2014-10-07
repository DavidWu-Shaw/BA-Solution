using System.Collections.Generic;
using Framework.Data;

namespace SubjectEngine.Data
{
	public class SubjectFieldData : ChildDataObject
	{        
		public SubjectFieldData()
		{
			SubjectFieldLanguagesData = new List<SubjectFieldLanguageData>();
		}

		public virtual string FieldKey { get; set; } 
		public virtual string FieldLabel { get; set; } 
		public virtual string HelpText { get; set; }
		public virtual DataTypeData FieldDataTypeData { get; set; }
		public virtual object PickupEntityId { get; set; }
		public virtual object LookupSubjectId { get; set; } 
		public virtual bool IsRequired { get; set; }
		public virtual bool IsBuiltIn { get; set; }
		public virtual bool IsReadonly { get; set; }
		public virtual bool IsShowInGrid { get; set; }
		public virtual bool IsLinkInGrid { get; set; }
		public virtual int Sort { get; set; }
		public virtual int RowIndex { get; set; }
		public virtual int ColIndex { get; set; }
		public virtual int? MaxLength { get; set; }
		public virtual string NavigateUrlFormatString { get; set; }

		public virtual IList<SubjectFieldLanguageData> SubjectFieldLanguagesData { get; set; }
	}
}
