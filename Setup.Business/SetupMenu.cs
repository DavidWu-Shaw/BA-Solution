using Framework.Business;
using Setup.Data;
using Framework.Validation;

namespace Setup.Business
{
    public class SetupMenu : BusinessObject<SetupMenuData>
    {
        [RequiredField("SetupMenuNameRequired", "The Name must be defined.")]
        [StringLength("SetupMenuNameLength", "The Name must have a length less than {1}", MaxLength = 50)]
        public string Name
        {
            get
            {
                return Data.Name;
            }
            set
            {
                Data.Name = value;
            }
        }

        public object ParentMenuId
        {
            get { return Data.ParentMenuId; }
            set { Data.ParentMenuId = value; }
        }

        [StringLength("SetupMenuMenuTextLength", "The MenuText must have a length less than {1}", MaxLength = 100)]
        public string MenuText
        {
            get
            {
                return Data.MenuText;
            }
            set
            {
                Data.MenuText = value;
            }
        }

        [StringLength("SetupMenuTooltipLength", "The Tooltip must have a length less than {1}", MaxLength = 200)]
        public string Tooltip
        {
            get
            {
                return Data.Tooltip;
            }
            set
            {
                Data.Tooltip = value;
            }
        }

        [StringLength("SetupMenuNavigateUrlLength", "The NavigateUrl must have a length less than {1}", MaxLength = 100)]
        public string NavigateUrl
        {
            get
            {
                return Data.NavigateUrl;
            }
            set
            {
                Data.NavigateUrl = value;
            }
        }

        public int Sort
        {
            get
            {
                return Data.Sort;
            }
            set
            {
                Data.Sort = value;
            }
        }

        public int MenuTypeId
        {
            get { return Data.MenuTypeId; }
            set { Data.MenuTypeId = value; }
        }
    }
}
