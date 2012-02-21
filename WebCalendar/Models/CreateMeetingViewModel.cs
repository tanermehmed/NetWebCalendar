using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCalendar.DAL;
using System.Web.Mvc;

namespace WebCalendar.Models
{
    public class CreateMeetingViewModel
    {
        public MeetingViewModel Meeting { get; set; }
        //public IQueryable<Contact> Contacts { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public CreateMeetingViewModel()
        {
        }

        //public Meeting ToMeeting()
        //{
            
        //}

        //public CreateMeetingViewModel(MeetingViewModel meeting, /*IQueryable<Contact> contacts,*/ List<SelectListItem> categories)
        //{
        //    this.Meeting = meeting;
        //    //this.Contacts = contacts;
        //    this.Categories = categories;
        //}
    }
}