using System.Linq;

namespace SportStoreCore2.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
        void Save(Product product);
    }
}
