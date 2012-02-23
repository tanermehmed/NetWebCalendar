using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalendar.DAL;
using WebCalendar.Models;
using System.Text.RegularExpressions;

namespace WebCalendar.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private IContactRepository repository;

        public ContactController(IContactRepository rep)
        {
            repository = rep;
        }

        //
        // GET: /Contact/

        public ViewResult Index()
        {
            List<ContactViewModel> contactsModel = new List<ContactViewModel>();
            IQueryable contacts = repository.All(User.Identity.Name);
            foreach (Contact contact in contacts)
            {
                contactsModel.Add(new ContactViewModel(contact));
            }
            return View(contactsModel);
        }

        //
        // GET: /Contact/Details/5

        public ViewResult Details(int id)
        {
            Contact contact = repository.Get(id);
            ContactViewModel contactModel = new ContactViewModel(contact);
            return View(contactModel);
        }

        //
        // GET: /Contact/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(ContactViewModel contactModel)
        {
            if (ModelState.IsValid)
            {
                Contact contact = contactModel.ToContact();
                repository.InsertOrUpdate(contact, User.Identity.Name);
                repository.Save();
                return RedirectToAction("Index");  
            }

            return View(contactModel);
        }
        
        //
        // GET: /Contact/Edit/5
 
        public ActionResult Edit(int id)
        {
            Contact contact = repository.Get(id);
            ContactViewModel contactModel = new ContactViewModel(contact);
            return View(contactModel);
        }

        //
        // POST: /Contact/Edit/5

        [HttpPost]
        public ActionResult Edit(ContactViewModel contactModel)
        {
            if (ModelState.IsValid)
            {
                Contact contact = contactModel.ToContact();
                repository.InsertOrUpdate(contact, User.Identity.Name);
                repository.Save();
                return RedirectToAction("Index");
            }

            return View(contactModel);
        }

        //
        // GET: /Contact/Delete/5
 
        public ActionResult Delete(int id)
        {
            Contact contact = repository.Get(id);
            ContactViewModel contactModel = new ContactViewModel(contact);
            return View(contactModel);
        }

        //
        // POST: /Contact/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = repository.Get(id);

            if (!contact.Meetings.Any())
            {
                repository.Delete(contact);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View("DeleteError");
        }
    }
}