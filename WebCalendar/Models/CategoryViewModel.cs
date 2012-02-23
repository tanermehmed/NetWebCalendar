using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebCalendar.DAL;
using System.ComponentModel.DataAnnotations;

namespace WebCalendar.Models
{
    public class CategoryViewModel
    {
        public int CategoryID { get; set; }
        [Required(ErrorMessage="Name is required!")]
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
            if (category != null)
            {
                this.CategoryID = category.CategoryID;
                this.Name = category.Name;
            }
            else
            {
                this.CategoryID = 0;
                this.Name = string.Empty;
            }
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