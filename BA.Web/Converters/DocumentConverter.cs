using System.Collections.Generic;
using CRM.Component.Dto;
using CRM.Data;
using Framework.Core;

namespace BA.Web.Converters
{
    public class DocumentConverter : IDataConverter<DocumentData, DocumentDto>
    {
        public IEnumerable<DocumentDto> Convert(IEnumerable<DocumentData> entitys)
        {
            List<DocumentDto> dtoList = new List<DocumentDto>();

            entitys.ForAll(e => dtoList.Add(Convert(e)));

            return dtoList;
        }

        public DocumentDto Convert(DocumentData entity)
        {
            DocumentDto dto = new DocumentDto();
            dto.Id = entity.Id;
            if (entity.Id != null)
            {
                dto.StringId = entity.Id.ToString();
            }
            dto.Display = entity.Name;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.Notes = entity.Notes;
            dto.FileName = entity.FileName;
            dto.Content = entity.Content;
            dto.Thumbnail = entity.Thumbnail;
            dto.DocTypeId = entity.DocTypeId;
            dto.IssuedById = entity.IssuedById;
            dto.IssuedDate = entity.IssuedDate;
            dto.Extension = entity.Extension;
            dto.ContentType = entity.ContentType;
            dto.ContentLength = entity.ContentLength;
            dto.LinkText = string.Format("{0}.{1}", dto.FileName, dto.Extension);

            return dto;
        }
    }
}
