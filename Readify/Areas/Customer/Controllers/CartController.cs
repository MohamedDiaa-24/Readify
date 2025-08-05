using Microsoft.AspNetCore.Mvc;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;
using Readify.Models.ViewModels;
using System.Security.Claims;

namespace Readify.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(S => S.ApplicationUserID == userId, includeProperties: "Product")

            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList)
            {
                cart.Price = GetPriceBaseOnQuantity(cart);
                ShoppingCartVM.OrderTotal += (cart.Price * cart.Count);
            }
            return View(ShoppingCartVM);
        }


        public IActionResult Plus(int cartId)
        {
            var cartFromDB = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
            cartFromDB.Count++;
            _unitOfWork.ShoppingCart.Update(cartFromDB);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFromDB = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);
            if (cartFromDB.Count <= 1)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDB);
            }
            else
            {
                cartFromDB.Count--;
                _unitOfWork.ShoppingCart.Update(cartFromDB);

            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFromDB = _unitOfWork.ShoppingCart.Get(c => c.Id == cartId);

            _unitOfWork.ShoppingCart.Remove(cartFromDB);

            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));

        }

        public IActionResult Summary()
        {

            return View();
        }

        private double GetPriceBaseOnQuantity(ShoppingCart shoppingCart)
        {

            if (shoppingCart.Count <= 50)
            {
                return shoppingCart.Product.Price;
            }
            else
            {
                if (shoppingCart.Count <= 100)
                {
                    return shoppingCart.Product.Price50;

                }
                else
                {
                    return shoppingCart.Product.Price100;

                }
            }
        }
    }
}
