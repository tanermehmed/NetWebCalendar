using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebCalendar.DAL;
using WebCalendar.Models;

namespace WebCalendar.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactRepository repository;

        public ContactController(IContactRepository rep)
        {
            repository = rep;
        }

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

        public ViewResult Details(int id)
        {
            Contact contact = repository.Get(id);
            ContactViewModel contactModel = new ContactViewModel(contact);
            return View(contactModel);
        }

        public ActionResult Create()
        {
            return View();
        } 

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

        public ActionResult Edit(int id)
        {
            Contact contact = repository.Get(id);
            ContactViewModel contactModel = new ContactViewModel(contact);
            return View(contactModel);
        }

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

        public ActionResult Delete(int id)
        {
            Contact contact = repository.Get(id);
            ContactViewModel contactModel = new ContactViewModel(contact);
            return View(contactModel);
        }

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