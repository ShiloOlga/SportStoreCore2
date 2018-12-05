using System.Linq;
using Moq;
using SportStoreCore2.Controllers;
using SportStoreCore2.Models;
using SportStoreCore2.Models.ViewModels;
using Xunit;

namespace SportStoreCore2.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Paginate()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            }).AsQueryable());
            var controller = new ProductController(mock.Object) { PageSize = 3 };
            // Act
            var result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            // Assert
            var prodArray = result.Products.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new[] {
                new Product {ProductId = 1, Name = "P1"},
                new Product {ProductId = 2, Name = "P2"},
                new Product {ProductId = 3, Name = "P3"},
                new Product {ProductId = 4, Name = "P4"},
                new Product {ProductId = 5, Name = "P5"}
            }).AsQueryable());
            // Arrange
            var controller = new ProductController(mock.Object) { PageSize = 3 };
            // Act
            var result = controller.List(null, 2).ViewData.Model as ProductsListViewModel;
            // Assert
            var pageInfo = result.PagingInfo;
            Assert.Equal(2, pageInfo.CurrentPage);
            Assert.Equal(3, pageInfo.ItemsPerPage);
            Assert.Equal(5, pageInfo.TotalItems);
            Assert.Equal(2, pageInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Products()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable());
            // Arrange
            var controller = new ProductController(mock.Object) { PageSize = 3 };
            // Act
            var result = (controller.List("Cat2", 1).ViewData.Model as ProductsListViewModel).Products.ToArray();
            // Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
        }
    }
}
