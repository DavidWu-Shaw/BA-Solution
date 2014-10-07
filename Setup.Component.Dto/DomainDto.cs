using System.Collections.Generic;
using Framework.Core;

namespace Setup.Component.Dto
{
    public class DomainDto : BaseDto
    {
        public string Name { get; set; }
        public string RelatedSubjectIdField { get; set; }
        public string DefaultUrl { get; set; }

        public IEnumerable<DomainMainMenuDto> DomainMainMenus { get; set; }
        public IEnumerable<DomainSetupMenuDto> DomainSetupMenus { get; set; }
    }
}
