using System.Collections.Generic;
using Framework.Core;

namespace Setup.Component.Dto
{
    public class LanguageDto : BaseDto
    {
        public string Name { get; set; }
        public string Label { get; set; }
        public string Culture { get; set; }
        public bool IsPublished { get; set; }

        public SortedDictionary<string, string> SysPhrases { get; set; }
        public SortedDictionary<string, string> DataPhrases { get; set; }
    }
}
