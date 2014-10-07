using Framework.Core;

namespace SubjectEngine.Component.Dto
{
    public class DEntityItemDto : BaseDto
    {
        # region Properties name

        public const string FLD_Value = "Value";
        public const string FLD_Text = "Text";

        # endregion Properties name

        public int Value { get; set; }
        public string Text { get; set; }
    }
}
