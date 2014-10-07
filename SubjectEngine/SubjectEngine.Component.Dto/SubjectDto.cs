using System.Collections.Generic;
using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class SubjectDto : BaseDto
    {
        # region Properties name

        public const string FLD_SubjectType = "SubjectType";
        public const string FLD_SubjectLabel = "SubjectLabel";
        public const string FLD_SubjectIdField = "SubjectIdField";
        public const string FLD_MasterSubjectIdField = "MasterSubjectIdField";
        public const string FLD_IsChildSubject = "IsChildSubject";
        public const string FLD_ManageUrl = "ManageUrl";
        public const string FLD_EditUrl = "EditUrl";
        public const string FLD_ListUrl = "ListUrl";
        public const string FLD_AllowListFiltering = "AllowListFiltering";
        public const string FLD_IsAddInGrid = "IsAddInGrid";
        public const string FLD_IsGridInFormEdit = "IsGridInFormEdit";
        public const string FLD_AllowListAdd = "AllowListAdd";
        public const string FLD_AllowListEdit = "AllowListEdit";
        public const string FLD_AllowListDelete = "AllowListDelete";
        public const string FLD_RowIndexMax = "RowIndexMax";
        public const string FLD_ColIndexMax = "ColIndexMax";

        # endregion Properties name

        public string SubjectType { get; set; }
        public string SubjectLabel { get; set; }
        public string SubjectIdField { get; set; }
        public string MasterSubjectIdField { get; set; }
        public bool IsChildSubject { get; set; }
        public string ManageUrl { get; set; }
        public string EditUrl { get; set; }
        public string ListUrl { get; set; }
        public bool AllowListFiltering { get; set; }
        public string ImportUrl { get; set; }
        public bool AllowListImport { get; set; }
        public bool IsAddInGrid { get; set; }
        public bool IsGridInFormEdit { get; set; }
        public bool AllowListAdd { get; set; }
        public bool AllowListEdit { get; set; }
        public bool AllowListDelete { get; set; }
        public int RowIndexMax { get; set; }
        public int ColIndexMax { get; set; }
        public IEnumerable<SubjectFieldDto> SubjectFields;
        public IEnumerable<SubjectChildListDto> SubjectChildLists;
        public IEnumerable<BaseDto> SubjectInstances;

        public IEnumerable<SubjectLanguageDto> SubjectLanguages { get; set; }
        public SortedDictionary<object, SubjectLanguageDto> SubjectLanguagesDic { get; set; }

        public string GetSubjectLabelByLanguage(object languageId)
        {
            string label = SubjectLabel;
            if (SubjectLanguagesDic.ContainsKey(languageId))
            {
                SubjectLanguageDto subjectLanguage = SubjectLanguagesDic[languageId];
                label = subjectLanguage.SubjectLabel;
            }
            return label;
        }
    }
}
