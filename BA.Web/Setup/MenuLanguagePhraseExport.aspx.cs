using System;
using BA.UnityRegistry;
using BA.Web.Common;
using Framework.UoW;
using General.Utility.Excel;
using Setup.Component;

namespace BA.Web.Setup
{
    public partial class MenuLanguagePhraseExport : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/MenuLanguagePhraseExport.aspx";

        private ExcelSheet Sheet { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucGrid.ListLabel = "Menu";

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
                Sheet = facade.GetMenuPhraseSheet();
            }
        }

        private void LoadData()
        {
            ucGrid.DataSource = Sheet.GetSampleData();
        }
    }
}