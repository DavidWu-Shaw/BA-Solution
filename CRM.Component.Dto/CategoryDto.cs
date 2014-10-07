using Framework.Core;

namespace CRM.Component.Dto
{
    public class CategoryDto : BaseDto
    {
        public string Name { get; set; }
        public object ParentId { get; set; }
        public object CatalogId { get; set; }
    }
}
