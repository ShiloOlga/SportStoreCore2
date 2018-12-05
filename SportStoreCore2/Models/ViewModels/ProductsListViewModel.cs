using System.Collections.Generic;

namespace SportStoreCore2.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string Category { get; set; }
    }
}
