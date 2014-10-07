using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;
using General.Utility.DataValidate;
using General.Utility.Excel;

namespace BA.Web.WebPages.Product
{
    public partial class ProductImport : SetupBasePage
    {
        public const string PageUrl = @"/WebPages/Product/ProductImport.aspx";

        #region Requested fields

        private const string Col_Name = "Product Name";
        private const string Col_Description = "Description";
        private const string Col_UnitPrice = "Unit Price";
        private const string Col_Packaging = "Packaging";
        private const string Col_UnitOfMeasure = "Unit of Measure";
        private const string Col_SupplierId = "Supplier";
        private const string Col_CategoryId = "Category";

        #endregion Requested fields

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Product Import";

            InitExcelImport();
        }

        protected override bool CheckPagePermission()
        {
            return CurrentUserContext.IsAdmin || CurrentUserContext.IsSupplierAdmin;
        }

        #region Init Controls

        private void InitExcelImport()
        {
            ucExcelImport.ExcelParsing += new ExcelParseEventHandler(ucExcelImport_ExcelParsing);
            ucExcelImport.ExcelImported += new ExcelImportEventHandler(ucExcelImport_ExcelImported);

            if (!IsPostBack)
            {
                List<ImportFieldMapping> importMappingList = new List<ImportFieldMapping>();
                InitImportMapping(importMappingList);
                ucExcelImport.MappingList = importMappingList;
            }
        }

        private void InitImportMapping(List<ImportFieldMapping> mappingList)
        {
            // Name
            ImportFieldMapping fullName = new ImportFieldMapping(Col_Name, true);
            fullName.Validator = new NotEmptyDataValidator();
            mappingList.Add(fullName);

            ImportFieldMapping desc = new ImportFieldMapping(Col_Description, false);
            mappingList.Add(desc);

            ImportFieldMapping price = new ImportFieldMapping(Col_UnitPrice, false);
            price.Validator = new DecimalDataValidator();
            mappingList.Add(price);

            ImportFieldMapping uom = new ImportFieldMapping(Col_UnitOfMeasure, false);
            mappingList.Add(uom);

            ImportFieldMapping packaging = new ImportFieldMapping(Col_Packaging, false);
            mappingList.Add(packaging);

            ImportFieldMapping supplierId = new ImportFieldMapping(Col_SupplierId, false);
            supplierId.Validator = new IntegerDataValidator();
            mappingList.Add(supplierId);

            ImportFieldMapping categoryId = new ImportFieldMapping(Col_CategoryId, false);
            categoryId.Validator = new IntegerDataValidator();
            mappingList.Add(categoryId);

        }

        #endregion Init Controls

        protected void ucExcelImport_ExcelParsing(object sender, ExcelParseEventArgs e)
        {
            if (!e.IsSuccessful)
            {
                AlertMessages(e.Message);
            }
        }

        protected void ucExcelImport_ExcelImported(object sender, ExcelImportEventArgs e)
        {
            PerformSave(e);
        }

        private void PerformSave(ExcelImportEventArgs arg)
        {
            ExcelSheet excelSheet = arg.Result;
            // Extract data from imported Excel sheet
            List<ProductDto> products = new List<ProductDto>();
            for (int index = excelSheet.DataStartRowIndex; index < excelSheet.Rows.Count; index++)
            {
                ExcelSheetRow excelRow = excelSheet.Rows[index];
                ProductDto product = new ProductDto();
                ExtractImportedSheetRow(product, excelRow);
                products.Add(product);
            }

            // Save batch
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ProductFacade facade = new ProductFacade(uow);
                IFacadeUpdateResult<ProductData> result = facade.SaveProducts(products);
                if (result.IsSuccessful)
                {
                    arg.IsSuccessful = true;
                    arg.Message = string.Format("Batch save done. \\nTotal {0} rows.", products.Count);
                }
                else
                {
                    arg.IsSuccessful = false;
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        // Extract data field by field
        private void ExtractImportedSheetRow(ProductDto product, ExcelSheetRow excelRow)
        {
            product.Name = excelRow.GetValue(Col_Name);
            product.Description = excelRow.GetValue(Col_Description);
            product.UnitPrice = Convert.ToDecimal(excelRow.GetValue(Col_UnitPrice));
            product.UnitOfMeasure = excelRow.GetValue(Col_UnitOfMeasure);
            product.Packaging = excelRow.GetValue(Col_Packaging);
            string supplierId = excelRow.GetValue(Col_SupplierId);
            if (supplierId != null && supplierId.TrimHasValue())
            {
                product.SupplierId = Convert.ToInt32(supplierId);
            }
            string categoryId = excelRow.GetValue(Col_CategoryId);
            if (categoryId != null && categoryId.TrimHasValue())
            {
                product.CategoryId = Convert.ToInt32(categoryId);
            }
        }
    }
}