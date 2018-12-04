using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SportStoreCore2.Models;
using SportStoreCore2.Models.ViewModels;

namespace SportStoreCore2.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;
        public int PageSize { get; set; } = 4;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int page = 1) => View(
            new ProductsListViewModel { 
                Products = _repository
                .Products.OrderBy(p => p.ProductId)
                .Skip(PageSize * (page - 1))
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                }
            });
    }
}
