using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.UoW;
using BA.UnityRegistry;
using CRM.Component;
using CRM.Component.Dto;
using System.IO;
using BA.Web.Converters;

namespace BA.Web.Sketch
{
    public partial class ShowSketch : System.Web.UI.Page
    {
        public const string QryProductId = "ProductId";

        private int? _productId;
        private int ProductId
        {
            get
            {
                if (_productId == null)
                {
                    _productId = -1;

                    int productId;
                    if (Request.QueryString[QryProductId] != null)
                    {
                        if (int.TryParse(Request.QueryString[QryProductId].ToString(), out productId))
                        {
                            _productId = productId;
                        }
                    }
                }

                return _productId.Value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // get sketch image
            byte[] buffer = GetImage(ProductId);

            if (buffer != null)
            {
                Response.ContentType = "image/jpeg";
                Response.BinaryWrite(buffer);
                Response.End();
            }
        }

        private byte[] GetImage(int productId)
        {
            byte[] image = null;

            if (productId != -1)
            {
                using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
                {
                    ProductFacade facade = new ProductFacade(uow);
                    ProductDto product = facade.RetrieveOrNewProduct(productId, new ProductConverter());
                    image = product.Sketch;
                }
            }

            if (image != null && image.Length > 0)
            {
                return image;
            }
            else
            {
                return GetImage();
            }
        }

        private byte[] GetImage()
        {
            // read from file
            FileStream aFileStream = File.OpenRead(this.Request.MapPath(Request.ApplicationPath + @"/Images/noImage.jpg"));
            long FileSize = aFileStream.Length;
            byte[] aBuffer = new byte[(int)FileSize];
            aFileStream.Read(aBuffer, 0, (int)FileSize);
            aFileStream.Flush();
            aFileStream.Close();

            return aBuffer;
        }
    }
}