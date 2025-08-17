using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;
using Readify.Utility;
using System.Security.Claims;

namespace Readify.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {

            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category");

            return View(products);
        }

        public IActionResult Details(int id)
        {
            ShoppingCart shoppingCart = new ShoppingCart
            {
                Product = _unitOfWork.Product.Get(p => p.Id == id, "Category"),
                Count = 1,
                ProductId = id
            };

            return View(shoppingCart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserID = userId;

            ShoppingCart cartFormDb = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserID == userId
            && c.ProductId == shoppingCart.ProductId);

            if (cartFormDb is not null)
            {
                cartFormDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFormDb);
                _unitOfWork.Save();

            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();

                HttpContext.Session.SetInt32(StaticDetails.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserID == userId).Count());
            }

            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
