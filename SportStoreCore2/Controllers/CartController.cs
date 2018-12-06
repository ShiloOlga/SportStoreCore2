using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore2.Models;
using SportStoreCore2.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportStoreCore2.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly Cart _cart;

        public CartController(IProductRepository repository, Cart cart)
        {
            _repository = repository;
            _cart = cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }

        // GET: /<controller>/
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                _cart.Remove(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
