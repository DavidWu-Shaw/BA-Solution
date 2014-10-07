using Framework.Business;
using Framework.Validation;
using SubjectEngine.Data;

namespace SubjectEngine.Business
{
    /// <summary>
    /// Summary description for EntityValue.
    /// </summary>
    public class DEntityItem : ChildBusinessObject<DEntity, DEntityItemData>
    {
        public int Value
        {
            get { return Data.Value; }
            set { Data.Value = value; }
        }

        [RequiredField("DEntityItemTextRequired", "The Text must be defined.")]
        [StringLength("DEntityItemTextLength", "The Text must have a length less than {1}", MaxLength = 100)]
        public string Text
        {
            get { return Data.Text; }
            set { Data.Text = value; }
        }
    }
}
