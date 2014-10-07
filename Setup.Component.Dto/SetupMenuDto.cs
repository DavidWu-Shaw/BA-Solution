using Framework.Core;

namespace Setup.Component.Dto
{
    public class SetupMenuDto : BaseDto
    {
        # region Properties name

        public const string FLD_Name = "Name";
        public const string FLD_ParentMenuId = "ParentMenuId";
        public const string FLD_MenuText = "MenuText";
        public const string FLD_Tooltip = "Tooltip";
        public const string FLD_NavigateUrl = "NavigateUrl";
        public const string FLD_Sort = "Sort";

        # endregion Properties name

        public string Name { get; set; }
        public object ParentMenuId { get; set; }
        public string MenuText { get; set; }
        public string Tooltip { get; set; }
        public string NavigateUrl { get; set; }
        public int Sort { get; set; }
        public int MenuTypeId { get; set; }
    }
}
