﻿using System;
using System.Linq;
using WebCalendar.DAL;
using System.ComponentModel.DataAnnotations;

namespace WebCalendar.Models
{
    public class ContactViewModel
    {
        public int ContactID { get; set; }

        [Required(ErrorMessage="First name is required")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string FirstName { get; set; }

        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-zA-Z0-9\\._-]+@[a-zA-Z0-9\\.-]{2,}[\\.][a-zA-Z]{2,4}$", ErrorMessage = "Please enter valid email address!")]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [DataType(DataType.DateTime, ErrorMessage="Please enter valid date and time!")]
        public DateTime? DateOfBirth { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Phone { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Address { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Comment { get; set; }

        public ContactViewModel()
        {
        }

        public ContactViewModel(int contactId, string firstName, string lastName, string email, DateTime? dateOfBirth, string phone, string address, string comment)
        {
            this.ContactID = contactId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.DateOfBirth = dateOfBirth;
            this.Phone = phone;
            this.Address = address;
            this.Comment = comment;
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
        }

        public Contact ToContact()
        {
            Contact contact = new Contact();
            contact.ContactID = this.ContactID;
            contact.FirstName = this.FirstName;
            contact.LastName = this.LastName;
            contact.Email = this.Email;
            contact.DateOfBirth = this.DateOfBirth;
            contact.Phone = this.Phone;
            contact.Address = this.Address;
            contact.Comment = this.Comment;

            return contact;
        }
    }
}