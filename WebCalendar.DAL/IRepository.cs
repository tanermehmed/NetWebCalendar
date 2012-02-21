using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebCalendar.DAL
{
    public interface IRepository<T>
    {
        void Delete(T item);
        void Save();
        T Get(int id);
        //IQueryable<T> All { get; }
        IQueryable<T> All(string username);
    }
}