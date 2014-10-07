using Framework.Core;

namespace PA.Component.Dto
{
    public class TaskDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public object OwnerId { get; set; }
        public int Duration { get; set; }
        public object PriorityId { get; set; }
        public object ProjectId { get; set; }
    }
}
