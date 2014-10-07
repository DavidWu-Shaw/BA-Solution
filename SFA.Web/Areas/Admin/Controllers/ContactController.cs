using System.Collections.Generic;
using System.Web.Mvc;
using BA.UnityRegistry;
using CRM.Component;
using CRM.Component.Dto;
using CRM.Core;
using Framework.UoW;
using SFA.Web.Areas.Admin.Controllers.Converters;
using SFA.Web.Areas.Admin.Models;

namespace SFA.Web.Areas.Admin.Controllers
{
    public class ContactController : InstanceController
    {
        public const string ControllerName = "Contact";
        public const string IndexAction = "Index";

        public ContactController()
        {
            InstanceType = InstanceTypes.Contact;
        }

        //
        // GET: /Admin/Contact/

        public ActionResult Index()
        {
            ListViewModel model = new ListViewModel(InstanceType);
            model.Instances = GetContactList();
            return ListView(model);
        }

        //
        // GET: /Admin/Contact/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Contact/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Contact/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Contact/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Contact/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Contact/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private IEnumerable<ContactDto> GetContactList()
        {
            using (IUnitOfWork uow = UnitOfWorkFactory.Instance.Start(DataStoreResolver.CRMDataStoreKey))
            {
                ContactFacade ContactFacade = new ContactFacade(uow);
                return ContactFacade.RetrieveAllContact(new ContactConverter());
            }
        }
    }
}
