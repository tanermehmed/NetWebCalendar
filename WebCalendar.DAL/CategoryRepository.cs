using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebCalendar.DAL
{
    public class CategoryRepository : ICategoryRepository
    {
        WebCalendarEntities db = new WebCalendarEntities();

        public IQueryable<Category> All(string username)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName.Equals(username));
            return db.Categories.Where(u => u.UserID == user.UserId);
        }

        public Category Get(int id)
        {
            return db.Categories.SingleOrDefault(c => c.CategoryID == id);
        }

        public void InsertOrUpdate(Category category, string username)
        {
            category.UserID = db.Users.SingleOrDefault(u => u.UserName == username).UserId;

            if (category.CategoryID == default(int))
            {
                db.Categories.AddObject(category);
            }
            else
            {
                db.Categories.Attach(category);
                db.ObjectStateManager.ChangeObjectState(category, EntityState.Modified);
            }
        }

        public void Delete(Category category)
        {
            if (!category.Meetings.Any())
            {
                db.Categories.DeleteObject(category);
            }
            else
            {
                throw new InvalidOperationException("This category cannot be deleted because has meetings.");
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}