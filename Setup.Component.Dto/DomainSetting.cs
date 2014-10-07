using System.Collections.Generic;
using Setup.Component.Dto;

namespace Setup.Component.Dto
{
    public class DomainSetting
    {
        public object DomainId { get; set; }
        public string Name { get; set; }
        public string RelatedSubjectIdField { get; set; }
        public string DefaultUrl { get; set; }

        public IEnumerable<MainMenuDto> MainMenus { get; set; }
        public IEnumerable<SetupMenuDto> SetupMenus { get; set; }
    }
}
