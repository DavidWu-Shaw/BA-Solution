using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class AttachmentEditItemTemplate : IBindableTemplate
    {
        private string ControlId { get; set; }
        private string DataField { get; set; }

        public AttachmentEditItemTemplate(string controlId, string dataField)
        {
            ControlId = controlId;
            DataField = dataField;
        }

        public void InstantiateIn(Control container)
        {
            RadUpload uploadAttachment = new RadUpload();
            container.Controls.Add(uploadAttachment);
            uploadAttachment.ID = ControlId;
            uploadAttachment.ControlObjectsVisibility = ControlObjectsVisibility.None;
        }

        #region IBindableTemplate Members

        public System.Collections.Specialized.IOrderedDictionary ExtractValues(Control container)
        {
            OrderedDictionary od = new OrderedDictionary();
            RadUpload uploadAttachment = (RadUpload)container.FindControl(ControlId);

            if (uploadAttachment.UploadedFiles.Count > 0)
            {
                UploadedFile fileStream = uploadAttachment.UploadedFiles[0];

                byte[] fileData = new byte[fileStream.ContentLength];
                fileStream.InputStream.Read(fileData, 0, fileStream.ContentLength);

                od.Add(DataField, fileData);
            }
            else
            {
                od.Add(DataField, null);
            }

            return od;
        }

        #endregion
    }
}