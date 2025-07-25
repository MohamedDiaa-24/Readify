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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();


            return View(products);
        }


        public IActionResult Upsert(int? id)//Upsert ==>[(Up)date][in(sert)]
        {
            IEnumerable<SelectListItem> categoryList = _unitOfWork.Category.GetAll()
                 .Select(c => new SelectListItem
                 {
                     Text = c.Name,
                     Value = c.Id.ToString()
                 });

            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = categoryList
            };

            if (id is null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(p => p.Id == id);
                return View(productVM);
            }


        }
        [HttpPost]
        public IActionResult Upsert(ProductVM model, IFormFile? file) //Upsert ==>[(Up)date][in(sert)]
        {

            string wwrootPath = _webHostEnvironment.WebRootPath;

            if (ModelState.IsValid)
            {
                if (file is not null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwrootPath, @"images\product");

                    if (!string.IsNullOrEmpty(model.Product.ImageURL))
                    {
                        string oldPath = Path.Combine(wwrootPath, model.Product.ImageURL);
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }


                    using (FileStream fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    model.Product.ImageURL = @"images\product\" + fileName;
                }

                if (model.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(model.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(model.Product);
                }

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


        #region Api Call Endpoint

        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = products });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productTobeDeleted = _unitOfWork.Product.Get(p => p.Id == id);

            if (productTobeDeleted is null)
            {
                return Json(new { success = false, message = "errror while deleting" });
            }
            string oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productTobeDeleted.ImageURL);
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productTobeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });

        }
        #endregion
    }
}
