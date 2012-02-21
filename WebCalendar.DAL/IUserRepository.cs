using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCalendar.DAL
{
    public interface IUserRepository
    {
        void Add(string username, string email, string firstname, string lastname);
        void Edit(int id);
        void Save();
    }
}
