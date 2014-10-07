using System.Collections.Generic;
using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class DEntityDto : BaseDto
    {
        # region Properties name

        public const string FLD_Code = "Code";
        public const string FLD_Description = "Description";
        public const string FLD_EntityTypeId = "EntityTypeId";
        public const string FLD_IsBuiltIn = "IsBuiltIn";
        public const string FLD_AllowAddItem = "AllowAddItem";
        public const string FLD_AllowEditItem = "AllowEditItem";
        public const string FLD_AllowDeleteItem = "AllowDeleteItem";

        # endregion Properties name

        public DEntityDto()
        {
            DEntityItems = new List<DEntityItemDto>();
        }

        public string Code { get; set; }
        public string Description { get; set; }
        public int? EntityTypeId { get; set; }
        public bool IsBuiltIn { get; set; }
        public bool AllowAddItem { get; set; }
        public bool AllowEditItem { get; set; }
        public bool AllowDeleteItem { get; set; }

        public IEnumerable<DEntityItemDto> DEntityItems;
    }
}
