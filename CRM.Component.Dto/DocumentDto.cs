using System;
using Framework.Core;

namespace CRM.Component.Dto
{
    public class DocumentDto : BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public string FileName { get; set; }
        public byte[] Content { get; set; }
        public byte[] Thumbnail { get; set; }
        public int DocTypeId { get; set; }
        public object IssuedById { get; set; }
        public DateTime IssuedDate { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public int ContentLength { get; set; }
        // For displaying attachment link text
        public string LinkText { get; set; }
    }
}
