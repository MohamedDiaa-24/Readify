using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;
using Readify.Models.ViewModels;
namespace Readify.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll().ToList();


            return View(products);
        }


        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll()
        .Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        });
            //ViewData["categoryList"] = categoryList;

            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = categoryList
            };


            return View(productVM);
        }
        [HttpPost]
        public IActionResult Create(ProductVM model)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Product.Add(model.Product);
                _unitOfWork.Save();
                TempData["success"] = "Product Created successfully";

                return RedirectToAction("Index", "Product");
            }
            else
            {
                model.CategoryList = _unitOfWork.Category.GetAll()
             .Select(c => new SelectListItem
             {
                 Text = c.Name,
                 Value = c.Id.ToString()
             });



                return View(model);
            }


        }

        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var Product = _unitOfWork.Product.Get(x => x.Id == id);
            if (Product is null)
            {
                return NotFound();
            }
            return View(Product);
        }
        [HttpPost]
        public IActionResult Edit(Product model)
        {

            if (ModelState.IsValid)
            {

                _unitOfWork.Product.Update(model);
                _unitOfWork.Save();
                TempData["success"] = "Product updated successfully";
                return RedirectToAction("Index", "Product");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            var Product = _unitOfWork.Product.Get(c => c.Id == id);
            if (Product is null)
            {
                return NotFound();
            }
            return View(Product);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var Product = _unitOfWork.Product.Get(x => x.Id == id);
            if (id is null || id == 0)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(Product);
            _unitOfWork.Save();
            TempData["success"] = "Product Deleted successfully";

            return RedirectToAction("Index", "Product");

        }
    }
}
