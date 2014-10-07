using Framework.Data;

namespace SubjectEngine.Data
{
	public class SubjectFieldInfoData : DataObject
	{
		public SubjectFieldInfoData()
		{
		}

		public virtual string TableColumn { get; set; }
		public virtual object SubjectFieldId { get; set; }
		public virtual string FieldKey { get; set; } 
		public virtual string FieldLabel { get; set; } 
		public virtual string HelpText { get; set; }
		public virtual object FieldDataTypeId { get; set; }
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
		public virtual int? MaxLengthInTable { get; set; }
		public virtual int? MaxLength { get; set; }
		public virtual string NavigateUrlFormatString { get; set; }
	}
}
