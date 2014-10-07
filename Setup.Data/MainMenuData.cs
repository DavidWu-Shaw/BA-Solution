using Framework.Data;
using System.Collections.Generic;

namespace Setup.Data
{
    public class MainMenuData : DataObject
    {
        public MainMenuData()
        {
        }

        public virtual string Name { get; set; }
        public virtual string MenuText { get; set; }
        public virtual string Tooltip { get; set; }
        public virtual string NavigateUrl { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual int Sort { get; set; }
    }
}
