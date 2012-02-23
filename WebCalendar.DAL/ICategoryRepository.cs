using System;
using System.Linq;

namespace WebCalendar.DAL
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void InsertOrUpdate(Category item, string username);
    }
}
