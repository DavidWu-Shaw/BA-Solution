using System;
using BA.Web.Common;
using Framework.UoW;
using BA.UnityRegistry;
using CRM.Component;
using CRM.Component.Dto;
using System.Web.UI;
using BA.Web.Converters;

namespace BA.Web.WebPages.Document
{
    public partial class DocumentDownload : Page
    {
        public const string PageUrl = @"/WebPages/Document/DocumentDownload.aspx";

        public const string QryDocumentId = "DocumentId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryDocumentId].TryToParse<int>();
            }
        }

        private DocumentDto CurrentDocument { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            RetrieveData();

            DownloadData();
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                DocumentFacade facade = new DocumentFacade(uow);
                CurrentDocument = facade.RetrieveOrNewDocument(InstanceId, new DocumentConverter());
            }
        }

        private void DownloadData()
        {
            if (CurrentDocument != null && CurrentDocument.Content != null)
            {
                string fileName = string.Format("{0}.{1}", CurrentDocument.FileName, CurrentDocument.Extension);
                string contentLength = CurrentDocument.ContentLength.ToString();

                Response.ClearContent();
                Response.ClearHeaders();
                Response.AddHeader("Content-Type", CurrentDocument.ContentType);
                Response.AddHeader("Content-Length", contentLength);
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.ContentType = CurrentDocument.ContentType;
                Response.BinaryWrite(CurrentDocument.Content);
                Response.Flush();
                Response.End();
            }
        }
    }
}