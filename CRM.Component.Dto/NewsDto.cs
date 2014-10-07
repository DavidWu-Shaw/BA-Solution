using System;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class NewsDto : BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public object IssuedById { get; set; }
        public DateTime IssuedTime { get; set; }
        public int? NewsGroupId { get; set; }
        public int? CategoryId { get; set; }
    }
}
