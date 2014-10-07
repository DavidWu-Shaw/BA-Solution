using Framework.Business;
using Framework.Validation;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
    public class DEntity : BusinessObject<DEntityData>
    {        
        protected override void OnInitialize()
        {
            base.OnInitialize();

            DEntityItems = new ChildBoCollection<DEntityData, DEntityItem, DEntityItemData>
                (Service, Data.DEntityItemsData, this);
        }

        [ChildCollection]
        public ChildBoCollection<DEntityData, DEntityItem, DEntityItemData> DEntityItems
        {
            get;
            private set;
        }

        [RequiredField("DEntityCodeRequired", "The Code must be defined.")]
        [StringLength("DEntityCodeLength", "The Code must have a length less than {1}", MaxLength = 100)]
        public string Code
        {
            get { return Data.Code; }
            set { Data.Code = value; }
        }

        [StringLength("DEntityDescriptionLength", "The Description must have a length less than {1}", MaxLength = 100)]
        public string Description
        {
            get { return Data.Description; }
            set { Data.Description = value; }
        }

        public int? EntityTypeId
        {
            get { return Data.EntityTypeId; }
            set { Data.EntityTypeId = value; }
        }

        public bool AllowAddItem
        {
            get { return Data.AllowAddItem; }
            set { Data.AllowAddItem = value; }
        }

        public bool AllowEditItem
        {
            get { return Data.AllowEditItem; }
            set { Data.AllowEditItem = value; }
        }

        public bool AllowDeleteItem
        {
            get { return Data.AllowDeleteItem; }
            set { Data.AllowDeleteItem = value; }
        }
    }
}
