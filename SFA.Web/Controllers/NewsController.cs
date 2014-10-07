using System.Collections.Generic;
using System.Web.Mvc;
using BA.UnityRegistry;
using CRM.Component;
using CRM.Component.Dto;
using Framework.UoW;
using SFA.Web.Models.Converters;

namespace SFA.Web.Controllers
{
    public class NewsController : BaseController
    {
        public const string ControllerName = "News";
        public const string IndexAction = "Index";
        public const string DetailAction = "Detail";

        // GET: /News List/
        public ActionResult Index()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                NewsFacade facade = new NewsFacade(uow);
                IEnumerable<NewsDto> news = facade.RetrieveAllNews(new NewsConverter());

                return View(news);
            }
        }

        public ActionResult Detail(int id)
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                NewsFacade facade = new NewsFacade(uow);
                NewsDto news = facade.RetrieveOrNewNews(id, new NewsConverter());

                return View(news);
            }
        }
    }
}
