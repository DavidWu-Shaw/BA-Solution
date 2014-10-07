using System;
using System.Collections.Generic;
using BA.UnityRegistry;
using BA.Web.Common;
using CRM.Component;
using Framework.UoW;
using General.Utility.Excel;
using Setup.Component;

namespace BA.Web.Setup
{
    public partial class SupplierLanguagePhrasesPage : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/SupplierLanguagePhrasesPage.aspx";

        private ExcelSheet Sheet { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucGrid.ListLabel = "Supplier";
            if (!IsPostBack)
            {
                RetrieveData();
                LoadData();
            }
        }

        private void RetrieveData()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                LanguageKeyFacade keyFacade = new LanguageKeyFacade(uow);
                Dictionary<string, string> phrases = keyFacade.GetSupplierLanguagePhrasesKeyValue();
                LanguageFacade facade = new LanguageFacade(uow);
                Sheet = facade.CreateDataPhraseSheet(phrases);
            }
        }

        private void LoadData()
        {
            ucGrid.DataSource = Sheet.GetSampleData();
        }
    }
}