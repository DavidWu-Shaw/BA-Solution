using System;
using Framework.Core;

namespace PA.Component.Dto
{
    public class TaskCommentDto: BaseDto
    {
        public string Comment { get; set; }
        public DateTime IssuedDate { get; set; }
        public int IssuedById { get; set; } 
    }
}
