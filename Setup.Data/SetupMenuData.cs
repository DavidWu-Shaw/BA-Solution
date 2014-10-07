using Framework.Data;

namespace Setup.Data
{
    public class SetupMenuData : DataObject
    {
        public SetupMenuData()
        {
        }

        public virtual string Name { get; set; }
        public virtual object ParentMenuId { get; set; }
        public virtual string MenuText { get; set; }
        public virtual string Tooltip { get; set; }
        public virtual string NavigateUrl { get; set; }
        public virtual int Sort { get; set; }
        public virtual int MenuTypeId { get; set; }
    }
}
