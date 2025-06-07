using Microsoft.AspNetCore.Mvc;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;
namespace Readify.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        public IActionResult Index()
        {
            var categories = _categoryRepo.GetAll().ToList();
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

                _categoryRepo.Add(model);
                _categoryRepo.Save();
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
            var category = _categoryRepo.Get(x => x.Id == id);
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

                _categoryRepo.Update(model);
                _categoryRepo.Save();
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
            var category = _categoryRepo.Get(c => c.Id == id);
            if (category is null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var category = _categoryRepo.Get(x => x.Id == id);
            if (id is null || id == 0)
            {
                return NotFound();
            }
            _categoryRepo.Remove(category);
            _categoryRepo.Save();
            TempData["success"] = "Category Deleted successfully";

            return RedirectToAction("Index", "Category");

        }
    }
}
