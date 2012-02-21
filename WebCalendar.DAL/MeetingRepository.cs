using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Objects.DataClasses;

namespace WebCalendar.DAL
{
    public class MeetingRepository : IMeetingRepository
    {
        WebCalendarEntities db = new WebCalendarEntities();

        public IQueryable<Meeting> All(string username)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName.Equals(username));
            return db.Meetings.Where(u => u.UserID == user.UserId);
        }

        public IQueryable<Meeting> FindUpcoming(string username)
        {
            DateTime todaysDate = DateTime.Now.Date;
            DateTime tomorrowsDate = todaysDate.AddDays(1);

            var user = db.Users.SingleOrDefault(x => x.UserName.Equals(username));
            IQueryable<Meeting> meetings = from meeting in db.Meetings
                                           where meeting.Time >= todaysDate && meeting.Time < tomorrowsDate && meeting.UserID == user.UserId
                                           orderby meeting.Time
                                           select meeting;
            return meetings;
        }

        public List<Meeting> FindByGivenCriteria(List<Meeting> meetings,
            DateTime? startDate, DateTime? endDate, string categoryName, string firstName, string lastName)
        {
            //var meetings = db.Meetings.ToList();

            if (startDate != null)
            {
                meetings = meetings.Where(m => m.Time > startDate).ToList();
            }

            if (endDate != null)
            {
                meetings = meetings.Where(m => m.Time < endDate).ToList();
            }

            if (!string.IsNullOrEmpty(categoryName))
            {
                meetings = meetings.Where(c => c.Category.Name == categoryName).ToList();
            }

            if (!string.IsNullOrEmpty(firstName))
            {
                List<Meeting> newMeetings = (from item in meetings 
                                             from c in item.Contacts 
                                             where c.FirstName.ToLower().Contains(firstName.ToLower()) 
                                             select item).ToList();
                meetings = newMeetings;
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                List<Meeting> newMeetings = (from item in meetings
                                             from c in item.Contacts
                                             where c.LastName.ToLower().Contains(lastName.ToLower())
                                             select item).ToList();
                meetings = newMeetings;
            }

            return meetings;
        }

        public Meeting Get(int id)
        {
            return db.Meetings.SingleOrDefault(m => m.MeetingID == id);
        }

        public void InsertOrUpdate(Meeting meeting, int[] contacts, string username)
        {
            meeting.UserID = db.Users.SingleOrDefault(u => u.UserName == username).UserId;

            if (meeting.MeetingID == default(int))
            {
                if (contacts != null)
                {
                    foreach (var contact in contacts)
                    {
                        Contact c = db.Contacts.SingleOrDefault(x => x.ContactID == contact);
                        meeting.Contacts.Add(c);
                    }
                }
                db.Meetings.AddObject(meeting);
            }
            else
            {
                Meeting m = db.Meetings.Include("Contacts").Single(x => x.MeetingID == meeting.MeetingID);
                m.Time = meeting.Time;
                m.Description = meeting.Description;
                m.Place = meeting.Place;
                m.CategoryID = meeting.CategoryID;
                m.Contacts.Clear();
                if (contacts != null)
                {
                    foreach (var contact in contacts)
                    {
                        Contact c = db.Contacts.SingleOrDefault(x => x.ContactID == contact);
                        m.Contacts.Add(c);
                    }
                }
                //db.Meetings.Attach(m);
                //db.ObjectStateManager.ChangeObjectState(m, EntityState.Modified);
            }
        }

        public void Delete(Meeting meeting)
        {
            Meeting m = db.Meetings.Include("Contacts").Single(x => x.MeetingID == meeting.MeetingID);
            db.Meetings.DeleteObject(m);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}