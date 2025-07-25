using Microsoft.AspNetCore.Mvc;
using Readify.DataAccess.Repository.Interfaces;
using Readify.Models;

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

            IEnumerable<Product> products = _unitOfWork.Product.GetAll("Category");

            return View(products);
        }

        public IActionResult Details(int id)
        {

            Product product = _unitOfWork.Product.Get(p => p.Id == id, "Category");

            return View(product);
        }
    }
}
