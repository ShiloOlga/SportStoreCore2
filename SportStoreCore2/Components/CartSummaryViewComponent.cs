using Microsoft.AspNetCore.Mvc;
using SportStoreCore2.Models;

namespace SportStoreCore2.Components
{
    public class CartSummaryViewComponent: ViewComponent
    {
        private readonly Cart _cart;

        public CartSummaryViewComponent(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}
