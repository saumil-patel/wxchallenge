using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Woolworths.Assessment.Enums;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;
using Woolworths.Assessment.Services.SortingStrategies;

namespace Woolworths.Assessment.TestProject
{
    public class StrategyTests
    {
        [Test]
        public async Task RecommendedStrategyTest()
        {
            var product1 = "Product 1";
            var product2 = "Product 2";
            var product3 = "Product 3";

            var shopperHistories = new List<ShopperHistory>
            {
                new ShopperHistory
                {
                    CustomerId = 1,
                    Products = new []{ new Product{ Name = product2, Quantity = 1}, new Product { Name = product3, Quantity = 1 } }
                },
                new ShopperHistory
                {
                    CustomerId = 2,
                    Products = new []{ new Product{ Name = product2, Quantity = 1} }
                }
            };

            var mockResourceProvider = SetupMockResourceProviderWithShoppingHistory(shopperHistories);

            var sortingStrategy = new RecommendedSortingStrategy(mockResourceProvider);

            Assert.AreEqual(SortOption.Recommended, sortingStrategy.GetSortType);

            List<Product> testUnsortedProducts = new List<Product>
            {
                new Product { Name = product1 },
                new Product { Name = product2 },
                new Product { Name = product3 }
            };

            var sortedProducts = (await sortingStrategy.GetSortedProducts(testUnsortedProducts)).ToList();

            Assert.AreEqual(testUnsortedProducts.Count, sortedProducts.Count);

            Assert.AreEqual(product2, sortedProducts[0].Name);
            Assert.AreEqual(product3, sortedProducts[1].Name);
            Assert.AreEqual(product1, sortedProducts[2].Name);
        }

        private static IWoolworthsResourceProvider SetupMockResourceProviderWithShoppingHistory(List<ShopperHistory> shopperHistories)
        {
            var mockResource = new Mock<IWoolworthsResourceProvider>();
            mockResource.Setup(r => r.GetShopperHistory()).ReturnsAsync(shopperHistories);

            var mockResourceProvider = mockResource.Object;
            return mockResourceProvider;
        }
    }
}