using System;
using BA.UnityRegistry;
using BA.Web.Common;
using BA.Web.Converters;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;

namespace BA.Web.Social
{
    public partial class NewsPage : AnonymousBasePage
    {
        public const string PageUrl = @"/Social/NewsPage.aspx";
        public const string QryInstanceId = "NewsId";

        private int? InstanceId
        {
            get
            {
                return Request.QueryString[QryInstanceId].TryToParse<int>();
            }
        }

        private NewsDto CurrentInstance { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            PageName = Localize("Social.NewsPage.PageName", "News Detail");

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
                NewsFacade facade = new NewsFacade(uow);
                CurrentInstance = facade.RetrieveOrNewNews(InstanceId, new NewsConverter());
            }
        }

        private void LoadData()
        {
            lblTitle.Text = CurrentInstance.Title;
            lblContent.Text = CurrentInstance.Content;
        }
    }
}