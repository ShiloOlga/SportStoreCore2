using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Routing;
using Moq;
using SportStoreCore2.Components;
using SportStoreCore2.Models;
using Xunit;

namespace SportStoreCore2.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            // Arrange
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable());
            var target = new NavigationMenuViewComponent(mock.Object);
            // Act
            var result = ((IEnumerable<string>) (target.Invoke() as ViewViewComponentResult).ViewData.Model).ToArray();
            ;
            // Assert
            Assert.True(new[] {"Cat1", "Cat2", "Cat3"}.SequenceEqual(result));
        }

        [Fact]
        public void Indicates_Selected_Category()
        {
            // Arrange
            var categoryToSelect = "Apples";
            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {ProductId = 1, Name = "P1", Category = "Apples"},
                new Product {ProductId = 4, Name = "P2", Category = "Oranges"},
            }).AsQueryable());
            var target = new NavigationMenuViewComponent(mock.Object);
            target.ViewComponentContext = new ViewComponentContext
            {
                ViewContext = new ViewContext
                {
                    RouteData = new RouteData()
                }
            };
            target.RouteData.Values["category"] = categoryToSelect;
            // Action
            var result = (string) (target.Invoke() as ViewViewComponentResult).ViewData["Category"];
            // Assert
            Assert.Equal(categoryToSelect, result);
        }
    }
}
