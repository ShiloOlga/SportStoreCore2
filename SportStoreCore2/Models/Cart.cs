using System.Collections.Generic;
using System.Linq;

namespace SportStoreCore2.Models
{
    public class Cart
    {
        private readonly List<CartLine> _lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines => _lineCollection;

        public virtual void AddItem(Product product, int quantity)
        {
            var line = _lineCollection.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
            if (line == null)
            {
                _lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void Remove(Product product)
        {
            _lineCollection.RemoveAll(p => p.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return _lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }

        public virtual void Clear()
        {
            _lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public int CartLineID { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
