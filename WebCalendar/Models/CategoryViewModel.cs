using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCalendar.DAL;

namespace WebCalendar.Models
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }

        public CategoryViewModel()
        {
        }

        public CategoryViewModel(int categoryId, string name)
        {
            this.CategoryID = categoryId;
            this.Name = name;
        }

        public CategoryViewModel(Category category)
        {
            this.CategoryID = category.CategoryID;
            this.Name = category.Name;
        }

        public Category ToCategory()
        {
            Category item = new Category();
            item.Name = this.Name;
            item.CategoryID = this.CategoryID;
            return item;
        }
    }
}