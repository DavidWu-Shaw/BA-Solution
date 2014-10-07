using System.Collections.Generic;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class CatalogDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual bool IsGlobal { get; set; }
        public virtual bool IsDiscontinued { get; set; }

        public IEnumerable<CategoryDto> CategoryList { get; set; }
    }
}
