using System;
using System.Collections.Generic;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;
using General.Utility.DataValidate;
using General.Utility.Excel;

namespace BA.Web.WebPages.Supplier
{
    public partial class SupplierImport : SetupBasePage
    {
        public const string PageUrl = @"/WebPages/Supplier/SupplierImport.aspx";

        #region Requested fields

        private const string Col_Name = "Name";
        private const string Col_Address = "Address";
        private const string Col_City = "City";
        private const string Col_Country = "Country";
        private const string Col_ZipCode = "ZipCode";
        private const string Col_ContactPerson = "Contact Person";
        private const string Col_ContactPhone = "Contact Phone";
        private const string Col_ContactFax = "Contact Fax";
        private const string Col_ContactEmail = "Contact Email";
        private const string Col_Website = "Website";
        private const string Col_Category = "Category";

        #endregion Requested fields

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Supplier Import";

            InitExcelImport();
        }

        protected override bool CheckPagePermission()
        {
            return CurrentUserContext.IsAdmin;
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
            ImportFieldMapping name = new ImportFieldMapping(Col_Name, true);
            name.Validator = new NotEmptyDataValidator();
            mappingList.Add(name);

            ImportFieldMapping address = new ImportFieldMapping(Col_Address, false);
            mappingList.Add(address);

            ImportFieldMapping colCity = new ImportFieldMapping(Col_City, false);
            mappingList.Add(colCity);

            ImportFieldMapping colCountry = new ImportFieldMapping(Col_Country, false);
            mappingList.Add(colCountry);

            ImportFieldMapping colZipCode = new ImportFieldMapping(Col_ZipCode, false);
            mappingList.Add(colZipCode);

            ImportFieldMapping colContactPerson = new ImportFieldMapping(Col_ContactPerson, false);
            mappingList.Add(colContactPerson);

            ImportFieldMapping contactPhone = new ImportFieldMapping(Col_ContactPhone, false);
            mappingList.Add(contactPhone);
            ImportFieldMapping contactFax = new ImportFieldMapping(Col_ContactFax, false);
            mappingList.Add(contactFax);
            ImportFieldMapping contactEmail = new ImportFieldMapping(Col_ContactEmail, false);
            mappingList.Add(contactEmail);
            ImportFieldMapping website = new ImportFieldMapping(Col_Website, false);
            mappingList.Add(website);

            ImportFieldMapping category = new ImportFieldMapping(Col_Category, false);
            category.Validator = new IntegerDataValidator();
            mappingList.Add(category);

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
            List<SupplierDto> Suppliers = new List<SupplierDto>();
            for (int index = excelSheet.DataStartRowIndex; index < excelSheet.Rows.Count; index++)
            {
                ExcelSheetRow excelRow = excelSheet.Rows[index];
                SupplierDto Supplier = new SupplierDto();
                ExtractImportedSheetRow(Supplier, excelRow);
                Suppliers.Add(Supplier);
            }

            // Save batch
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                SupplierFacade facade = new SupplierFacade(uow);
                IFacadeUpdateResult<SupplierData> result = facade.SaveSuppliers(Suppliers);
                if (result.IsSuccessful)
                {
                    arg.IsSuccessful = true;
                    arg.Message = string.Format("Batch save done. \\nTotal {0} rows.", Suppliers.Count);
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
        private void ExtractImportedSheetRow(SupplierDto Supplier, ExcelSheetRow excelRow)
        {
            Supplier.Name = excelRow.GetValue(Col_Name);
            Supplier.AddressLine1 = excelRow.GetValue(Col_Address);
            Supplier.City = excelRow.GetValue(Col_City);
            Supplier.Country = excelRow.GetValue(Col_Country);
            Supplier.ZipCode = excelRow.GetValue(Col_ZipCode);
            Supplier.ContactPerson = excelRow.GetValue(Col_ContactPerson);
            Supplier.ContactPhone = excelRow.GetValue(Col_ContactPhone);
            Supplier.ContactFax = excelRow.GetValue(Col_ContactFax);
            Supplier.ContactEmail = excelRow.GetValue(Col_ContactEmail);
            Supplier.Website = excelRow.GetValue(Col_Website);
            string categoryId = excelRow.GetValue(Col_Category);
            if (categoryId != null && categoryId.TrimHasValue())
            {
                Supplier.CategoryId = Convert.ToInt32(categoryId);
            }
        }
    }
}