using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCalendar.DAL;
using System.Data.Objects.DataClasses;

namespace WebCalendar.Models
{
    public class ContactViewModel
    {
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Comment { get; set; }
        //public List<Meeting> Meetings { get; set; }

        public ContactViewModel()
        {
        }

        public ContactViewModel(int contactId, string firstName, string lastName, string email, DateTime? dateOfBirth, string phone, string address, string comment/*, List<Meeting> meetings*/)
        {
            this.ContactID = contactId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
            this.Phone = phone;
            this.Address = address;
            this.Comment = comment;
            //this.Meetings = meetings;
        }

        public ContactViewModel(Contact contact)
        {
            this.ContactID = contact.ContactID;
            this.FirstName = contact.FirstName;
            this.LastName = contact.LastName;
            this.Email = contact.Email;
            this.DateOfBirth = contact.DateOfBirth;
            this.Phone = contact.Phone;
            this.Address = contact.Address;
            this.Comment = contact.Comment;
            //this.Meetings = contact.Meetings.ToList();
        }

        public Contact ToContact()
        {
            //EntityCollection<Meeting> meetingsCollection = new EntityCollection<Meeting>();
            //foreach (var meeting in this.Meetings)
            //{
            //    meetingsCollection.Add(meeting);
            //}

            Contact contact = new Contact();
            contact.ContactID = this.ContactID;
            contact.FirstName = this.FirstName;
            contact.LastName = this.LastName;
            contact.Email = this.Email;
            contact.DateOfBirth = this.DateOfBirth;
            contact.Phone = this.Phone;
            contact.Address = this.Address;
            contact.Comment = this.Comment;
            //contact.Meetings = meetingsCollection;

            return contact;
        }
    }
}