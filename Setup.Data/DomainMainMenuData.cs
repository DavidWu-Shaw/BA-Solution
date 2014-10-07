using Framework.Data;

namespace Setup.Data
{
    public class DomainMainMenuData : ChildDataObject
    {
        public virtual object MainMenuId { get; set; }
        public virtual int Sort { get; set; }
    }
}
