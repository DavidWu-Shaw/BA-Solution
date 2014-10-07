using System;
using System.Collections.Generic;
using Framework.Component;
using BA.UnityRegistry;
using BA.Web.Common;
using Framework.Core;
using Framework.UoW;
using General.Utility.DataValidate;
using General.Utility.Excel;
using Setup.Component;
using Setup.Component.Dto;
using Setup.Data;

namespace BA.Web.Setup
{
    public partial class LanguagePhraseImport : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/LanguagePhraseImport.aspx";

        #region Requested fields

        private const string Col_Key = "Phrase Key";
        private const string Col_Value = "Phrase Value";

        #endregion Requested fields

        private object _selectedLanguageId;
        private object SelectedLanguageId
        {
            get
            {
                if (_selectedLanguageId == null)
                {
                    _selectedLanguageId = Convert.ToInt32(ddlLanguage.SelectedValue);
                }
                return _selectedLanguageId;
            }
        }

        private bool IsDataPhrase
        {
            get
            {
                return rbData.Checked;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDDL();
            }

            InitExcelImport();
        }

        private void LoadDDL()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                LanguageFacade facade = new LanguageFacade(uow);
                ddlLanguage.DataSource = facade.GetBindingList();
            }

            ddlLanguage.DataValueField = BindingListItem.ValueProperty;
            ddlLanguage.DataTextField = BindingListItem.TextProperty;
            ddlLanguage.DataBind();
        }

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
            ImportFieldMapping colKey = new ImportFieldMapping(Col_Key, true);
            colKey.Validator = new NotEmptyDataValidator();
            mappingList.Add(colKey);

            ImportFieldMapping colValue = new ImportFieldMapping(Col_Value, true);
            colValue.Validator = new NotEmptyDataValidator();
            mappingList.Add(colValue);
        }

        protected void ucExcelImport_ExcelParsing(object sender, ExcelParseEventArgs e)
        {
            if (!e.IsSuccessful)
            {
                AlertMessages(e.Message);
            }
        }

        protected void ucExcelImport_ExcelImported(object sender, ExcelImportEventArgs e)
        {
            if (IsDataPhrase)
            {
                SaveAsDataPhrases(e);
            }
            else
            {
                SaveAsSystemPhrases(e);
            }
        }

        private void SaveAsDataPhrases(ExcelImportEventArgs arg)
        {
            ExcelSheet excelSheet = arg.Result;
            // Extract data from imported Excel sheet
            List<DataPhraseDto> dataPhrases = new List<DataPhraseDto>();
            for (int index = excelSheet.DataStartRowIndex; index < excelSheet.Rows.Count; index++)
            {
                ExcelSheetRow excelRow = excelSheet.Rows[index];
                DataPhraseDto dataPhrase = new DataPhraseDto();
                dataPhrases.Add(dataPhrase);
                dataPhrase.PhraseKey = excelRow.GetValue(Col_Key);
                dataPhrase.PhraseValue = excelRow.GetValue(Col_Value);
            }

            // Save batch
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                LanguageFacade facade = new LanguageFacade(uow);
                IFacadeUpdateResult<DataPhraseData> result = facade.SaveDataPhrases(SelectedLanguageId, dataPhrases);
                if (result.IsSuccessful)
                {
                    arg.IsSuccessful = true;
                    arg.Message = string.Format("Batch save done. \\nTotal {0} rows.", dataPhrases.Count);
                }
                else
                {
                    arg.IsSuccessful = false;
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        private void SaveAsSystemPhrases(ExcelImportEventArgs arg)
        {
            ExcelSheet excelSheet = arg.Result;
            // Extract data from imported Excel sheet
            List<LanguagePhraseDto> languagePhrases = new List<LanguagePhraseDto>();
            for (int index = excelSheet.DataStartRowIndex; index < excelSheet.Rows.Count; index++)
            {
                ExcelSheetRow excelRow = excelSheet.Rows[index];
                LanguagePhraseDto languagePhrase = new LanguagePhraseDto();
                languagePhrases.Add(languagePhrase);
                languagePhrase.PhraseKey = excelRow.GetValue(Col_Key);
                languagePhrase.PhraseValue = excelRow.GetValue(Col_Value);
            }

            // Save batch
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                LanguageFacade facade = new LanguageFacade(uow);
                IFacadeUpdateResult<LanguagePhraseData> result = facade.SaveSystemPhrases(SelectedLanguageId, languagePhrases);
                if (result.IsSuccessful)
                {
                    arg.IsSuccessful = true;
                    arg.Message = string.Format("Batch save done. \\nTotal {0} rows.", languagePhrases.Count);
                }
                else
                {
                    arg.IsSuccessful = false;
                    // Deal with Update result
                    ProcUpdateResult(result.ValidationResult, result.Exception);
                }
            }
        }

        protected override void ImplementLabelLocalization()
        {
            base.ImplementLabelLocalization();
            lblTitle.Text = Localize("Setup.LanguagePhraseImport.lblTitle", "Import multi-language phrases");
            lblLang.Text = Localize("Setup.LanguagePhraseImport.lblLang", "Select language for import");
            rbData.Text = Localize("Setup.LanguagePhraseImport.rbData", "Data phrases");
            rbSystem.Text = Localize("Setup.LanguagePhraseImport.rbSystem", "System phrases");
        }
    }
}