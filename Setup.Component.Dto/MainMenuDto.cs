using Framework.Core;

namespace Setup.Component.Dto
{
    public class MainMenuDto : BaseDto
    {
        public string Name { get; set; }
        public string MenuText { get; set; }
        public string Tooltip { get; set; }
        public string NavigateUrl { get; set; }
        public string ImageUrl { get; set; }
        public int Sort { get; set; }
    }
}
