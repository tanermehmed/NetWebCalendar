using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCalendar.DAL;
using System.Data.Objects.DataClasses;

namespace WebCalendar.DAL
{
    public interface IMeetingRepository : IRepository<Meeting>
    {
        void InsertOrUpdate(Meeting item, List<int> contacts, string username);
        IQueryable<Meeting> All(string username);
        List<Meeting> FindByGivenCriteria(List<Meeting> meetings, DateTime? startDate, DateTime? endDate, string categoryName, string firstName, string lastName);
        IQueryable<Meeting> FindUpcoming(string username);
    }
}