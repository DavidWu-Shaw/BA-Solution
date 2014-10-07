using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Common.Helper;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Data;
using Framework.UoW;
using General.Utility.DataValidate;
using General.Utility.Excel;

namespace BA.Web.WebPages.Contact
{
    public partial class ContactImport : SetupBasePage
    {
        public const string PageUrl = @"/WebPages/Contact/ContactImport.aspx";

        #region Requested fields

        private const string Col_Row = "Row";
        private const string Col_Validation = "Validation";
        private const string Col_FullName = "Full Name";
        private const string Col_FamilyName = "Family Name";
        private const string Col_Gender = "Gender";
        private const string Col_Phone = "Phone";
        private const string Col_Email = "Email";
        private const string Col_Fax = "Fax";
        private const string Col_ZipCode = "ZipCode";
        private const string Col_AddressLine1 = "Address Line 1";
        private const string Col_AddressLine2 = "Address Line 2";
        private const string Col_City = "City";
        private const string Col_Country = "Country";
        private const string Col_CountryState = "Country State";

        #endregion Requested fields

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = "Contact Import";

            InitExcelImport();
        }

        protected override bool CheckPagePermission()
        {
            return CurrentUserContext.IsEmployee;
        }

        #region Init Controls

        private void InitExcelImport()
        {
            ucExcelImport.ExcelParsing += new ExcelParseEventHandler(ucExcelImport_ExcelParsing);
            ucExcelImport.ExcelSheetValidating += new ExcelSheetValidatingEventHandler(ucExcelImport_ExcelSheetValidating);
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
            // Full Name
            ImportFieldMapping fullName = new ImportFieldMapping(Col_FullName, true);
            fullName.Validator = new NotEmptyDataValidator();
            mappingList.Add(fullName);

            // familyName
            ImportFieldMapping familyName = new ImportFieldMapping(Col_FamilyName, false);
            mappingList.Add(familyName);

            // gender
            ImportFieldMapping gender = new ImportFieldMapping(Col_Gender, false);
            char[] regChars = new char[] { 'M', 'F' };
            gender.Validator = new CharSetValidator(regChars);
            mappingList.Add(gender);

            // phone
            ImportFieldMapping phone = new ImportFieldMapping(Col_Phone, false);
            mappingList.Add(phone);

            // Col_Email
            ImportFieldMapping email = new ImportFieldMapping(Col_Email, false);
            mappingList.Add(email);

            // Col_Fax
            ImportFieldMapping fax = new ImportFieldMapping(Col_Fax, false);
            mappingList.Add(fax);

            // Col_ZipCode
            ImportFieldMapping zipCode = new ImportFieldMapping(Col_ZipCode, false);
            mappingList.Add(zipCode);

            // Col_AddressLine1
            ImportFieldMapping addressLine1 = new ImportFieldMapping(Col_AddressLine1, false);
            mappingList.Add(addressLine1);
            // Col_AddressLine1
            ImportFieldMapping addressLine2 = new ImportFieldMapping(Col_AddressLine2, false);
            mappingList.Add(addressLine2);

            // Col_City
            ImportFieldMapping city = new ImportFieldMapping(Col_City, false);
            mappingList.Add(city);

            // Col_Country
            ImportFieldMapping country = new ImportFieldMapping(Col_Country, false);
            mappingList.Add(country);

            // Col_CountryState
            ImportFieldMapping countryState = new ImportFieldMapping(Col_CountryState, false);
            mappingList.Add(countryState);
        }

        #endregion Init Controls

        protected void ucExcelImport_ExcelParsing(object sender, ExcelParseEventArgs e)
        {
            if (!e.IsSuccessful)
            {
                AlertMessages(e.Message);
            }
        }

        protected void ucExcelImport_ExcelSheetValidating(object sender, ExcelSheetValidatingEventArgs e)
        {
            // Perform whole Excel Sheet level validation
            ValidateExcelSheet(e.Sheet);
        }

        protected void ucExcelImport_ExcelSheetRowValidating(object sender, ExcelSheetRowValidatingEventArgs e)
        {
            // Perform Excel Sheet row level validation
        }

        protected void ucExcelImport_ExcelImported(object sender, ExcelImportEventArgs e)
        {
            PerformSave(e);
        }

        protected void ucImportResult_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                object status = DataBinder.Eval(e.Row.DataItem, Col_Validation);
                if (status != null && status.ToString().Length > 0)
                {
                    e.Row.BackColor = Color.Orange;
                }
            }
        }

        private void ValidateExcelSheet(ExcelSheet sheet)
        {
            // Check duplicated FullName
        }

        private void PerformSave(ExcelImportEventArgs arg)
        {
            ExcelSheet excelSheet = arg.Result;
            // Extract data from imported Excel sheet
            List<ContactDto> contacts = new List<ContactDto>();
            foreach (ExcelSheetRow excelRow in excelSheet.Rows)
            {
                ContactDto contact = new ContactDto();
                ExtractImportedSheetRow(contact, excelRow);
                contact.EmployeeId = CurrentUserContext.User.MatchId;
                contacts.Add(contact);
            }

            // Save batch
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ContactFacade facade = new ContactFacade(uow);
                IFacadeUpdateResult<ContactData> result = facade.SaveContacts(contacts);
                if (result.IsSuccessful)
                {
                    arg.Message = string.Format("Batch save done. \\nTotal {0} rows.", contacts.Count);
                }
                else
                {
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        // Extract data field by field
        private void ExtractImportedSheetRow(ContactDto contact, ExcelSheetRow excelRow)
        {
            contact.FullName = excelRow.GetValue(Col_FullName);
            contact.FamilyName = excelRow.GetValue(Col_FamilyName);
        }
    }
}