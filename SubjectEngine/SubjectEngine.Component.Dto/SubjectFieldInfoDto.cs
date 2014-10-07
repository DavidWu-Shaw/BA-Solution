using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class SubjectFieldInfoDto : BaseDto
    {
        # region Properties name

        public const string FLD_TableColumn = "TableColumn";
        public const string FLD_SubjectFieldId = "SubjectFieldId";
        public const string FLD_FieldKey = "FieldKey";
        public const string FLD_FieldLabel = "FieldLabel";
        public const string FLD_HelpText = "HelpText";
        public const string FLD_FieldDataTypeId = "FieldDataTypeId";
        public const string FLD_PickupEntityId = "PickupEntityId";
        public const string FLD_LookupSubjectId = "LookupSubjectId";
        public const string FLD_IsRequired = "IsRequired";
        public const string FLD_IsReadonly = "IsReadonly";
        public const string FLD_IsShowInGrid = "IsShowInGrid";
        public const string FLD_IsLinkInGrid = "IsLinkInGrid";
        public const string FLD_Sort = "Sort";
        public const string FLD_RowIndex = "RowIndex";
        public const string FLD_ColIndex = "ColIndex";
        public const string FLD_MaxLengthInTable = "MaxLengthInTable";
        public const string FLD_MaxLength = "MaxLength";
        public const string FLD_NavigateUrlFormatString = "NavigateUrlFormatString";

        # endregion Properties name

        public string TableColumn { get; set; }
        public object SubjectFieldId { get; set; }
        public string FieldKey { get; set; }
        public string FieldLabel { get; set; }
        public string HelpText { get; set; }
        public object FieldDataTypeId { get; set; }
        public object PickupEntityId { get; set; }
        public object LookupSubjectId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsReadonly { get; set; }
        public bool IsShowInGrid { get; set; }
        public bool IsLinkInGrid { get; set; }
        public int Sort { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public int? MaxLengthInTable { get; set; }
        public int? MaxLength { get; set; }
        public string NavigateUrlFormatString { get; set; }
    }
}
