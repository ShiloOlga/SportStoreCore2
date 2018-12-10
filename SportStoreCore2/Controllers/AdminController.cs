using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore2.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SportStoreCore2.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        // GET: /<controller>/
        public ViewResult Index() => View(_repository.Products);

        public ViewResult Edit(int productId) => View(_repository.Products.FirstOrDefault(p => p.ProductId == productId));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _repository.Save(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");
            }
            return View(product);
        }

        public ViewResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            var deletedProduct = _repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SeedDatabase()
        {
            SeedData.EnsurePopulated(HttpContext.RequestServices);
            return RedirectToAction(nameof(Index));
        }
    }
}
