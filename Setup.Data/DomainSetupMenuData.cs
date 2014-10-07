using Framework.Data;

namespace Setup.Data
{
    public class DomainSetupMenuData : ChildDataObject
    {
        public virtual object SetupMenuId { get; set; }
        public virtual int Sort { get; set; }
    }
}
