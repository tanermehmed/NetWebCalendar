using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebCalendar.DAL;
using WebCalendar.Models;

namespace WebCalendar.Controllers
{
    [Authorize]
    public class MeetingController : Controller
    {
        private readonly IMeetingRepository meetingRepository;
        private readonly IContactRepository contactRepository;
        private readonly ICategoryRepository categoryRepository;

        public MeetingController(IMeetingRepository rep, IContactRepository contactRep, ICategoryRepository categoryRep)
        {
            meetingRepository = rep;
            contactRepository = contactRep;
            categoryRepository = categoryRep;
        }

        public ViewResult Index()
        {
            IQueryable<Meeting> meetings = meetingRepository.FindUpcoming(User.Identity.Name);
            List<MeetingViewModel> meetingsModel = new List<MeetingViewModel>();
            List<ContactViewModel> contactList = new List<ContactViewModel>();

            foreach (var meeting in meetings)
            {
                foreach (var contact in meeting.Contacts)
                {
                    contactList.Add(new ContactViewModel(contact));
                }
                meetingsModel.Add(new MeetingViewModel(meeting));
            }

            ViewBag.categoryName = new SelectList(categoryRepository.All(User.Identity.Name), "Name", "Name");

            return View(meetingsModel);
        }

        [HttpPost]
        public ViewResult Index(DateTime? startDate, DateTime? endDate, string categoryName, string firstName, string lastName)
        {

            List<Meeting> meetings = meetingRepository.FindUpcoming(User.Identity.Name).ToList();
            List<MeetingViewModel> meetingsModel = new List<MeetingViewModel>();
            List<ContactViewModel> contactList = new List<ContactViewModel>();

            meetings = meetingRepository.FindByGivenCriteria(meetings,startDate, endDate, categoryName, firstName, lastName);

            foreach (var meeting in meetings)
            {
                foreach (var contact in meeting.Contacts)
                {
                    contactList.Add(new ContactViewModel(contact));
                }
                meetingsModel.Add(new MeetingViewModel(meeting));
            }

            ViewBag.categoryName = new SelectList(categoryRepository.All(User.Identity.Name), "Name", "Name");

            return View(meetingsModel);
        }

        public ViewResult AllMeetings()
        {
            IQueryable<Meeting> meetings = meetingRepository.All(User.Identity.Name);
            List<MeetingViewModel> meetingsModel = new List<MeetingViewModel>();
            List<ContactViewModel> contactList = new List<ContactViewModel>();

            foreach (var meeting in meetings)
            {
                foreach (var contact in meeting.Contacts)
                {
                    contactList.Add(new ContactViewModel(contact));
                }
                meetingsModel.Add(new MeetingViewModel(meeting));
            }

            ViewBag.categoryName = new SelectList(categoryRepository.All(User.Identity.Name), "Name", "Name");

            return View(meetingsModel);
        }

        [HttpPost]
        public ViewResult AllMeetings(DateTime? startDate, DateTime? endDate, string categoryName, string firstName, string lastName)
        {

            List<Meeting> meetings = meetingRepository.All(User.Identity.Name).ToList();
            List<MeetingViewModel> meetingsModel = new List<MeetingViewModel>();
            List<ContactViewModel> contactList = new List<ContactViewModel>();

            meetings = meetingRepository.FindByGivenCriteria(meetings, startDate, endDate, categoryName, firstName, lastName);

            foreach (var meeting in meetings)
            {
                foreach (var contact in meeting.Contacts)
                {
                    contactList.Add(new ContactViewModel(contact));
                }
                meetingsModel.Add(new MeetingViewModel(meeting));
            }

            ViewBag.categoryName = new SelectList(categoryRepository.All(User.Identity.Name), "Name", "Name");

            return View(meetingsModel);
        }

        public ActionResult Create()
        {
            IQueryable<Contact> contacts = contactRepository.All(User.Identity.Name);
            List<ContactViewModel> contactsModel = new List<ContactViewModel>();
            foreach (var item in contacts)
            {
                contactsModel.Add(new ContactViewModel(item));
            }

            List<Category> categoriesDal = categoryRepository.All(User.Identity.Name).ToList();
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            foreach (var item in categoriesDal)
            {
                categories.Add(new CategoryViewModel(item));
            }
            ViewBag.CategoryID = new SelectList(categories, "CategoryID", "Name");

            return View(new MeetingViewModel(contactsModel));
        }

        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            MeetingViewModel meetingModel = new MeetingViewModel();
            if (ModelState.IsValid)
            {
                meetingModel.Time = DateTime.Parse(form["Time"]);
                meetingModel.Description = form["Description"];
                meetingModel.Place = form["Place"];

                string category = form["CategoryID"];
                GetActiveCategory(meetingModel, category);

                string[] checkedValues = form.GetValues("assignChkBx");
                List<int> contactIds = GetActiveContacts(checkedValues);

                Meeting meeting = meetingModel.ToMeeting();
                meetingRepository.InsertOrUpdate(meeting, contactIds, User.Identity.Name);
                meetingRepository.Save();
                return RedirectToAction("Index");
            }

            return View(meetingModel);
        }

        private void GetActiveCategory(MeetingViewModel meetingModel, string category)
        {
            IQueryable<Category> cat = categoryRepository.All(User.Identity.Name);
            foreach (var item in cat)
            {
                if (item.Name == category)
                {
                    meetingModel.Category = new CategoryViewModel(item);
                    break;
                }
            }
        }

        private static List<int> GetActiveContacts(string[] checkedValues)
        {
            List<int> contactIds = new List<int>();
            if (checkedValues != null)
            {
                for (int i = 0; i < checkedValues.Length; i++)
                {
                    contactIds.Add(int.Parse(checkedValues[i]));
                }
            }

            return contactIds;
        }

        public ActionResult Edit(int id)
        {
            Meeting meeting = meetingRepository.Get(id);
            MeetingViewModel meetingModel = new MeetingViewModel(meeting);

            List<Category> categoriesDal = categoryRepository.All(User.Identity.Name).ToList();
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            foreach (var item in categoriesDal)
            {
                categories.Add(new CategoryViewModel(item));
            }

            List<Contact> contactsDal = contactRepository.All(User.Identity.Name).ToList();
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            foreach (var item in contactsDal)
            {
                contacts.Add(new ContactViewModel(item));
            }

            ViewBag.Contacts = contacts;
            ViewBag.CategoryID = new SelectList(categories, "Name", "Name", 
                                                meeting.Category != null ? meeting.Category.Name : string.Empty);
            return View(meetingModel);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection form,int id)
        {
            MeetingViewModel meetingModel = new MeetingViewModel();
            if (ModelState.IsValid)
            {
                meetingModel.MeetingID = id;
                meetingModel.Time = DateTime.Parse(form["Time"]);
                meetingModel.Description = form["Description"];
                meetingModel.Place = form["Place"];

                string category = form["CategoryID"];
                GetActiveCategory(meetingModel, category);

                string[] checkedValues = form.GetValues("assignChkBx");
                List<int> contactIds = GetActiveContacts(checkedValues);

                Meeting meeting = meetingModel.ToMeeting();
                meetingRepository.InsertOrUpdate(meeting, contactIds, User.Identity.Name);
                meetingRepository.Save();

                return RedirectToAction("Index");
            }

            return View(meetingModel);
        }

        public ActionResult Delete(int id)
        {
            Meeting meeting = meetingRepository.Get(id);
            MeetingViewModel meetingModel = new MeetingViewModel(meeting);

            return View(meetingModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Meeting meeting = meetingRepository.Get(id);
            meetingRepository.Delete(meeting);
            meetingRepository.Save();

            return RedirectToAction("AllMeetings");
        }
    }
}