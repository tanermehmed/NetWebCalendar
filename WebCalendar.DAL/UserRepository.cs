using System;
using System.Linq;

namespace WebCalendar.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly WebCalendarEntities db = new WebCalendarEntities();

        public void Add(string username, string email, string firstname, string lastname)
        {
            MyUser user = new MyUser();
            User copyUser = db.Users.SingleOrDefault(x => x.UserName == username);
            user.UserId = copyUser.UserId;
            user.Username = username;
            user.Email = email;
            user.FirstName = firstname;
            user.LastName = lastname;
            db.MyUsers.AddObject(user);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
