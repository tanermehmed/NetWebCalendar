using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebCalendar.DAL
{
    public class ContactRepository : IContactRepository
    {
        WebCalendarEntities db = new WebCalendarEntities();

        public IQueryable<Contact> All(string username)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName.Equals(username));
            return db.Contacts.Where(u => u.UserID == user.UserId);
        }

        public Contact Get(int id)
        {
            return db.Contacts.SingleOrDefault(c => c.ContactID == id);
        }

        public void InsertOrUpdate(Contact contact, string username)
        {
            var user = db.Users.SingleOrDefault(u => u.UserName == username);
            contact.UserID = user.UserId;

            if (contact.ContactID == default(int))
            { 
                if (contact.DateOfBirth != null)
                {
                    int currentYear = DateTime.Now.Year;
                    int currentMonth = DateTime.Now.Month;

                    DateTime? birthday = contact.DateOfBirth;

                    if (currentMonth > birthday.Value.Month)
                    {
                        currentYear++;
                    }

                    DateTime meetingTime = new DateTime(
                        currentYear,
                        birthday.Value.Month,
                        birthday.Value.Day - 1,
                        14, 0, 0);

                    if(!db.Categories.Any(x=>x.Name=="Birthday" && x.UserID == user.UserId))
                    {
                        Category category = new Category();
                        category.Name = "Birthday";
                        category.UserID = db.Users.SingleOrDefault(u => u.UserName == username).UserId;
                        db.Categories.AddObject(category);
                        db.SaveChanges();
                    }

                    Meeting meeting = new Meeting();
                    meeting.UserID = user.UserId;
                    meeting.CategoryID = db.Categories.SingleOrDefault(c => c.Name == "Birthday" && c.UserID == user.UserId).CategoryID;
                    meeting.Time = meetingTime;
                    meeting.Description = "Congratulate " + contact.FirstName + "!";
                    meeting.Contacts.Add(contact);
                    db.Meetings.AddObject(meeting);
                }
                db.Contacts.AddObject(contact);
            }
            else
            {
                db.Contacts.Attach(contact);
                db.ObjectStateManager.ChangeObjectState(contact, EntityState.Modified);
            }
        }

        public void Delete(Contact contact)
        {
            db.Contacts.DeleteObject(contact);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}