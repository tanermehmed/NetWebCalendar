using System;
using System.Linq;

namespace WebCalendar.DAL
{
    public interface IRepository<T>
    {
        void Delete(T item);
        void Save();
        T Get(int id);
        IQueryable<T> All(string username);
    }
}