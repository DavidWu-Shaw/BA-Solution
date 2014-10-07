using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BA.Web.Common;
using General.Utility.Excel;
using Framework.UoW;
using BA.UnityRegistry;
using SubjectEngine.Component;
using Setup.Component;

namespace BA.Web.Setup
{
    public partial class SubjectLanguageMgt : AdminSetupBasePage
    {
        public const string PageUrl = @"/Setup/SubjectLanguageMgt.aspx";

        private ExcelSheet Sheet { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            ucGrid.ListLabel = "Subject";
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
                Dictionary<string, string> phrases = keyFacade.GetSubjectLanguagePhrasesKeyValue();
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