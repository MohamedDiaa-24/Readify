using Microsoft.AspNetCore.Mvc;
using Readify.DataAccess.Data;
using Readify.Models;

namespace Readify.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dBContext;

        public CategoryController(ApplicationDBContext applicationDBContext)
        {
            _dBContext = applicationDBContext;
        }
        public IActionResult Index()
        {
            var categories = _dBContext.categories.ToList();
            return View(categories);
        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category model)
        {

            if (ModelState.IsValid)
            {

                _dBContext.categories.Add(model);
                _dBContext.SaveChanges();
                TempData["success"] = "Category Created successfully";

                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var category = _dBContext.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {

            if (ModelState.IsValid)
            {

                _dBContext.categories.Update(model);
                _dBContext.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var category = _dBContext.categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var category = _dBContext.categories.Find(id);
            if (id is null || id == 0)
            {
                return NotFound();
            }

            _dBContext.categories.Remove(category);
            _dBContext.SaveChanges();
            TempData["success"] = "Category Deleted successfully";

            return RedirectToAction("Index", "Category");

        }
    }
}
