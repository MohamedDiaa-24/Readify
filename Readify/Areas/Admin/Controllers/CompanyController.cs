using Microsoft.AspNetCore.Mvc;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;
namespace Readify.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = StaticDetails.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var Companys = _unitOfWork.Company.GetAll().ToList();


            return View(Companys);
        }


        public IActionResult Upsert(int? id)
        {


            if (id is null || id == 0)
            {
                //Create
                return View(new Company());
            }
            else
            {
                //Update
                Company Company = _unitOfWork.Company.Get(c => c.ID == id);
                return View(Company);
            }


        }
        [HttpPost] // [UPSERT Satnd for Udate and Insert]
        public IActionResult Upsert(Company model)
        {


            if (ModelState.IsValid)
            {


                if (model.ID == 0)
                {
                    // Create
                    _unitOfWork.Company.Add(model);
                }
                else
                {
                    // Update
                    _unitOfWork.Company.Update(model);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company Created successfully";

                return RedirectToAction("Index", "Company");
            }
            else
            {

                return View(model);
            }


        }


        #region Api Call Endpoint

        [HttpGet]
        public IActionResult GetAll()
        {
            var Companies = _unitOfWork.Company.GetAll().ToList();

            return Json(new { data = Companies });
        }




        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var CompanyTobeDeleted = _unitOfWork.Company.Get(p => p.ID == id);

            if (CompanyTobeDeleted is null)
            {
                return Json(new { success = false, message = "errror while deleting" });
            }


            _unitOfWork.Company.Remove(CompanyTobeDeleted);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful" });

        }
        #endregion
    }
}
