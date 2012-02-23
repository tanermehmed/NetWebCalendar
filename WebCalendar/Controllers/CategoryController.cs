using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebCalendar.DAL;
using WebCalendar.Models;

namespace WebCalendar.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private ICategoryRepository repository;

        public CategoryController(ICategoryRepository rep)
        {
            repository = rep;
        }

        //
        // GET: /Categories/

        public ViewResult Index()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();
            IQueryable<Category> category = repository.All(User.Identity.Name);
            foreach (var item in category)
            {
                categories.Add(new CategoryViewModel(item));

            }
            return View(categories);
        }

        //
        // GET: /Categories/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Categories/Create

        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category category = categoryModel.ToCategory();
                repository.InsertOrUpdate(category, User.Identity.Name);
                repository.Save();
                return RedirectToAction("Index");
            }

            return View(categoryModel);
        }

        //
        // GET: /Categories/Edit/5

        public ActionResult Edit(int id)
        {
            Category category = repository.Get(id);
            CategoryViewModel categoryModel = new CategoryViewModel(category);
            return View(categoryModel);
        }

        //
        // POST: /Categories/Edit/5

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                Category category = categoryModel.ToCategory();
                repository.InsertOrUpdate(category, User.Identity.Name);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }

        //
        // GET: /Categories/Delete/5

        public ActionResult Delete(int id)
        {
            Category category = repository.Get(id);
            CategoryViewModel categoryModel = new CategoryViewModel(category);
            return View(categoryModel);
        }

        //
        // POST: /Categories/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = repository.Get(id);
            bool isCategoryNotEmpty = category.Meetings.Any();
            if (!isCategoryNotEmpty)
            {
                repository.Delete(category);
                repository.Save();
                return RedirectToAction("Index");
            }
            return View("DeleteError");
        }
    }
}