using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore2.Infrastructure;
using SportStoreCore2.Models;
using SportStoreCore2.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportStoreCore2.Controllers
{
    public class CartController : Controller
    {
        private readonly IProductRepository _repository;

        public CartController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: /<controller>/
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            var product = _repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.AddItem(product, 1);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            var product = _repository.Products
                .FirstOrDefault(p => p.ProductId == productId);
            if (product != null)
            {
                var cart = GetCart();
                cart.Remove(product);
                SaveCart(cart);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

        private Cart GetCart()
        {
            var cart = HttpContext.Session.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        private void SaveCart(Cart cart)
        {
            HttpContext.Session.SetJson("Cart", cart);
        }
    }
}
