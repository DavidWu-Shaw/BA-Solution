using Framework.Core;

namespace PA.Component.Dto
{
    public class TaskTimeDto: BaseDto
    {
        public int UserId { get; set; }
        public int TimeSpent { get; set; }
    }
}
