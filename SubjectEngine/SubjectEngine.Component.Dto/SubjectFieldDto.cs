using System.Collections.Generic;
using SubjectEngine.Core;
using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class SubjectFieldDto : BaseDto
    {
        public string FieldKey { get; set; }
        public string FieldLabel { get; set; }
        public string HelpText { get; set; }
        public object FieldDataTypetId { get; set; }
        public DucTypes DucType { get; set; }
        public object PickupEntityId { get; set; }
        public object LookupSubjectId { get; set; }
        public bool IsRequired { get; set; }
        public bool IsReadonly { get; set; }
        public bool IsShowInGrid { get; set; }
        public bool IsLinkInGrid { get; set; }
        public int Sort { get; set; }
        public int RowIndex { get; set; }
        public int ColIndex { get; set; }
        public int? MaxLength { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public string NavigateUrlFormatString { get; set; }
        public IEnumerable<BindingListItem> ListDataSource { get; set; }
        public string LookupSubjectManageUrlFormatString { get; set; }
        public string LookupSubjectType { get; set; }

        public IEnumerable<SubjectFieldLanguageDto> SubjectFieldLanguages { get; set; }
        public SortedDictionary<object, SubjectFieldLanguageDto> SubjectFieldLanguagesDic { get; set; }

        public string GetFieldLabelByLanguage(object languageId)
        {
            string label = FieldLabel;
            if (SubjectFieldLanguagesDic.ContainsKey(languageId))
            {
                SubjectFieldLanguageDto subjectFieldLanguage = SubjectFieldLanguagesDic[languageId];
                label = subjectFieldLanguage.FieldLabel;
            }
            return label;
        }

        public string GetHelpTextByLanguage(object languageId)
        {
            string label = HelpText;
            if (SubjectFieldLanguagesDic.ContainsKey(languageId))
            {
                SubjectFieldLanguageDto subjectFieldLanguage = SubjectFieldLanguagesDic[languageId];
                label = subjectFieldLanguage.HelpText;
            }
            return label;
        }
    }
}
