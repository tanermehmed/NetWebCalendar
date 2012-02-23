using System;
using System.Linq;

namespace WebCalendar.DAL
{
    public interface IContactRepository : IRepository<Contact>
    {
        void InsertOrUpdate(Contact item, string username);
    }
}
