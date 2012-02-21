using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCalendar.DAL
{
    public interface IContactRepository : IRepository<Contact>
    {
        void InsertOrUpdate(Contact item, string username);
    }
}
