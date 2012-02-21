using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCalendar.DAL
{
    public class UserRepository : IUserRepository
    {
        WebCalendarEntities db = new WebCalendarEntities();

        public void Add(string username, string email, string firstname, string lastname)
        {
            MyUser user = new MyUser();
            User user1 = db.Users.SingleOrDefault(x => x.UserName == username);
            user.UserId = user1.UserId;
            user.Username = username;
            user.Email = email;
            user.FirstName = firstname;
            user.LastName = lastname;
            db.MyUsers.AddObject(user);
        }

        public void Edit(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
