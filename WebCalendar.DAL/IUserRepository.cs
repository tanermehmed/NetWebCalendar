using System;
using System.Linq;

namespace WebCalendar.DAL
{
    public interface IUserRepository
    {
        void Add(string username, string email, string firstname, string lastname);
        void Save();
    }
}
