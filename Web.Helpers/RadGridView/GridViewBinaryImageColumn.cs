using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;

namespace BA.Web.Common.Helper
{
    public class GridViewBinaryImageColumn : GridViewColumn
    {
        public string DataFieldKey { get; set; }
        public bool IsReadOnly { get; set; }
        public int ImageWidth { get; set; }
        public int ImageHeight { get; set; }

        public GridViewBinaryImageColumn(string caption, string dataFieldKey)
        {
            Caption = string.IsNullOrEmpty(caption) ? dataFieldKey : caption;
            DataFieldKey = dataFieldKey;
            UniqueName = dataFieldKey;
        }

        public override GridColumn CreateColumn()
        {
            GridBinaryImageColumn column = new GridBinaryImageColumn();
            return column;
        }

        public override void AttachProperties(GridColumn gridColumn)
        {
            base.AttachProperties(gridColumn);
            GridBinaryImageColumn column = (GridBinaryImageColumn)gridColumn;
            column.DataField = DataFieldKey;
            column.ReadOnly = IsReadOnly;
            column.ResizeMode = BinaryImageResizeMode.Fit;
            column.ImageWidth = ImageWidth;
            column.ImageHeight = ImageHeight;
            column.AllowFiltering = false;
            column.AllowSorting = false;
        }
    }
}