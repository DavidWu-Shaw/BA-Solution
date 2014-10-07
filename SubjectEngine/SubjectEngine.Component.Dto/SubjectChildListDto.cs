using System.Collections.Generic;
using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class SubjectChildListDto : BaseDto
    {
        # region Properties name

        public const string FLD_ChildListSubjectId = "ChildListSubjectId";
        public const string FLD_ChildListSubjectType = "ChildListSubjectType";
        public const string FLD_Title = "Title";
        public const string FLD_AllowAdd = "AllowAdd";
        public const string FLD_AllowEdit = "AllowEdit";
        public const string FLD_AllowDelete = "AllowDelete";
        public const string FLD_Sort = "Sort";

        # endregion Properties name

        public object ChildListSubjectId { get; set; }
        public string Title { get; set; }
        public bool AllowAdd { get; set; }
        public bool AllowEdit { get; set; }
        public bool AllowDelete { get; set; }
        public int Sort { get; set; }
        public bool AllowFiltering { get; set; }
        public string ImportUrl { get; set; }
        public bool AllowImport { get; set; }

        public SubjectDto ChildListSubject { get; set; }
        public IEnumerable<SubjectChildListLanguageDto> SubjectChildListLanguages { get; set; }
        public SortedDictionary<object, SubjectChildListLanguageDto> SubjectChildListLanguagesDic { get; set; }

        public string GetTitleByLanguage(object languageId)
        {
            string label = Title;
            if (SubjectChildListLanguagesDic.ContainsKey(languageId))
            {
                SubjectChildListLanguageDto item = SubjectChildListLanguagesDic[languageId];
                label = item.Title;
            }
            return label;
        }
    }
}
