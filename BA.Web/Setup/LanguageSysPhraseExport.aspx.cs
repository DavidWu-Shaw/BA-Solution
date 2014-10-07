using System;
using BA.UnityRegistry;
using BA.Web.Common;
using Framework.UoW;
using General.Utility.Excel;
using Setup.Component;

namespace BA.Web.Setup
{
    public partial class LanguageSysPhraseExport : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/LanguageSysPhraseExport.aspx";

        private ExcelSheet Sheet { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucGrid.ListLabel = "SystemPhrase";

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
                LanguageFacade facade = new LanguageFacade(uow);
                Sheet = facade.CreateSystemPhraseSheet(WebContext.Current.ApplicationOption.DefaultLanguageId);
            }
        }

        private void LoadData()
        {
            ucGrid.DataSource = Sheet.GetSampleData();
        }
    }
}