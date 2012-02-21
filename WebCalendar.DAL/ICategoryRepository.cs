using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCalendar.DAL
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void InsertOrUpdate(Category item, string username);
    }
}
