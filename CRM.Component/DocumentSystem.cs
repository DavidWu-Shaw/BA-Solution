using System.Collections.Generic;
using System.Linq;
using CRM.Business;
using CRM.Component.Dto;
using CRM.Data;
using CRM.Service.Contract;
using Framework.Component;
using Framework.Core;
using Framework.UoW;

namespace CRM.Component
{
    internal class DocumentSystem : BusinessComponent
    {
        public DocumentSystem(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        internal List<TDto> RetrieveAllDocument<TDto>(IDataConverter<DocumentData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IDocumentService service = UnitOfWork.GetService<IDocumentService>();

            var query = service.GetAll();

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal List<TDto> RetrieveDocumentsByUser<TDto>(object instanceId, IDataConverter<DocumentData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);
            ArgumentValidator.IsNotNull("converter", converter);
            IDocumentService service = UnitOfWork.GetService<IDocumentService>();

            var query = service.SearchByUser(instanceId);

            if (query.HasResult)
            {
                return query.DataToDtoList(converter).ToList();
            }

            return null;
        }

        internal TDto RetrieveOrNewDocument<TDto>(object instanceId, IDataConverter<DocumentData, TDto> converter)
            where TDto : class
        {
            ArgumentValidator.IsNotNull("converter", converter);
            IDocumentService service = UnitOfWork.GetService<IDocumentService>();
            FacadeUpdateResult<DocumentData> result = new FacadeUpdateResult<DocumentData>();
            Document instance = RetrieveOrNew<DocumentData, Document, IDocumentService>(result.ValidationResult, instanceId);
            if (result.IsSuccessful)
            {
                return converter.Convert(instance.RetrieveData<DocumentData>());
            }
            else
            {
                return null;
            }
        }

        internal IFacadeUpdateResult<DocumentData> SaveDocument(DocumentDto dto)
        {
            ArgumentValidator.IsNotNull("dto", dto);

            FacadeUpdateResult<DocumentData> result = new FacadeUpdateResult<DocumentData>();
            IDocumentService service = UnitOfWork.GetService<IDocumentService>();
            Document instance = RetrieveOrNew<DocumentData, Document, IDocumentService>(result.ValidationResult, dto.Id);

            if (result.IsSuccessful)
            {
                instance.Name = dto.Name;
                instance.Description = dto.Description;
                instance.Notes = dto.Notes;
                instance.FileName = dto.FileName;
                instance.Content = dto.Content;
                instance.Thumbnail = MakeThumbnail(dto.Content);
                instance.DocTypeId = dto.DocTypeId;
                instance.IssuedById = dto.IssuedById;
                instance.IssuedDate = dto.IssuedDate;
                instance.Extension = dto.Extension;
                instance.ContentType = GetContentType(dto.Extension);
                instance.ContentLength = dto.Content.Length;

                var saveQuery = service.Save(instance);

                result.AttachResult(instance.RetrieveData<DocumentData>());
                result.Merge(saveQuery);
            }

            return result;
        }

        private string GetContentType(string extension)
        {
            string contentType = string.Empty;

            switch (extension)
            {
                case "htm":
                case "html":
                case "log":
                    contentType = "text/HTML";
                    break;
                case "txt":
                    contentType = "text/plain";
                    break;
                case "doc":
                    contentType = "application/ms-word";
                    break;
                case "tiff":
                case "tif":
                    contentType = "image/tiff";
                    break;
                case "asf":
                    contentType = "video/x-ms-asf";
                    break;
                case "avi":
                    contentType = "video/avi";
                    break;
                case "zip":
                    contentType = "application/zip";
                    break;
                case "xls":
                case "csv":
                    contentType = "application/vnd.ms-excel";
                    break;
                case "gif":
                    contentType = "image/gif";
                    break;
                case "jpg":
                case "jpeg":
                    contentType = "image/jpeg";
                    break;
                case "bmp":
                    contentType = "image/bmp";
                    break;
                case "wav":
                    contentType = "audio/wav";
                    break;
                case "mp3":
                    contentType = "audio/mpeg3";
                    break;
                case "mpg":
                case "mpeg":
                    contentType = "video/mpeg";
                    break;
                case "rtf":
                    contentType = "application/rtf";
                    break;
                case "asp":
                    contentType = "text/asp";
                    break;
                case "pdf":
                    contentType = "application/pdf";
                    break;
                case "fdf":
                    contentType = "application/vnd.fdf";
                    break;
                case "ppt":
                    contentType = "application/mspowerpoint";
                    break;
                case "dwg":
                    contentType = "image/vnd.dwg";
                    break;
                case "msg":
                    contentType = "application/msoutlook";
                    break;
                case "xml":
                case "sdxl":
                    contentType = "application/xml";
                    break;
                case "xdp":
                    contentType = "application/vnd.adobe.xdp+xml";
                    break;
                default:
                    contentType = "application/octet-stream";
                    break;
            }

            return contentType;

        }

        private byte[] MakeThumbnail(byte[] content)
        {
            return null;
        }

        internal IFacadeUpdateResult<DocumentData> DeleteDocument(object instanceId)
        {
            ArgumentValidator.IsNotNull("instanceId", instanceId);

            FacadeUpdateResult<DocumentData> result = new FacadeUpdateResult<DocumentData>();
            IDocumentService service = UnitOfWork.GetService<IDocumentService>();
            var query = service.Retrieve(instanceId);
            if (query.HasResult)
            {
                Document instance = query.ToBo<Document>();
                var saveQuery = instance.Delete();
                result.Merge(saveQuery);
            }
            else
            {
                AddError(result.ValidationResult, "DocumentCannotBeFound");
            }

            return result;
        }

        internal IList<BindingListItem> GetBindingList()
        {
            List<BindingListItem> dataSource = new List<BindingListItem>();
            IDocumentService service = UnitOfWork.GetService<IDocumentService>();
            var query = service.GetAll();
            if (query.HasResult)
            {
                foreach (DocumentData data in query.DataList)
                {
                    dataSource.Add(new BindingListItem(data.Id, data.Name));
                }
            }

            return dataSource;
        }
    }
}
