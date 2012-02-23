using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCalendar.DAL;
using System.Data.Objects.DataClasses;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebCalendar.Models
{
    public class MeetingViewModel
    {
        public int MeetingID { get; set; }

        [Required(ErrorMessage="Date and time are required")]
        public DateTime? Time { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string Place { get; set; }
        public CategoryViewModel Category { get; set; }
        public List<ContactViewModel> ContactModels { get; set; }

        public MeetingViewModel()
        {
        }

        public MeetingViewModel(List<ContactViewModel> contacts)
        {
            this.ContactModels = contacts;
        }

        public MeetingViewModel(int meetingID, DateTime? time, string description, string place, CategoryViewModel category, List<ContactViewModel> contacts)
        {
            this.MeetingID = meetingID;
            this.Time = time;
            this.Description = description;
            this.Place = place;
            this.Category = category;
            this.ContactModels = new List<ContactViewModel>(contacts);
        }

        public MeetingViewModel(Meeting meeting)
        {
            List<ContactViewModel> contactsCollection = new List<ContactViewModel>();
            foreach (var item in meeting.Contacts)
            {              
                contactsCollection.Add(new ContactViewModel(item));
            }

            this.MeetingID = meeting.MeetingID;
            this.Time = meeting.Time;
            this.Description = meeting.Description;
            this.Place = meeting.Place;
            this.Category = new CategoryViewModel(meeting.Category);
            this.ContactModels = contactsCollection;
        }

        public Meeting ToMeeting()
        {
            Meeting meeting = new Meeting();

            CategoryViewModel categoryModel = new CategoryViewModel();
            categoryModel = this.Category;
            Category category = new Category();
            if (this.Category == null)
            {
                meeting.CategoryID = null;
            }
            else
            {
                category = categoryModel.ToCategory();
                meeting.CategoryID = category.CategoryID;
            }

            meeting.MeetingID = this.MeetingID;
            meeting.Time = this.Time;
            meeting.Description = this.Description;
            meeting.Place = this.Place;
            
            return meeting;
        }
    }
}