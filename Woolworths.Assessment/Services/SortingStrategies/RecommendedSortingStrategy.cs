using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Woolworths.Assessment.Enums;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;

namespace Woolworths.Assessment.Services.SortingStrategies
{
    public class RecommendedSortingStrategy : IProductSortingStrategy
    {
        private readonly IWoolworthsResourceProvider _woolworthsResourceProvider;

        public RecommendedSortingStrategy(IWoolworthsResourceProvider woolworthsResourceProvider)
        {
            _woolworthsResourceProvider = woolworthsResourceProvider;
        }

        public SortOption GetSortType => SortOption.Recommended;

        public async Task<IEnumerable<Product>> GetSortedProducts(IEnumerable<Product> unsortedProducts)
        {
            var productQuantityLookup = await GetProductQuantityLookup();

            var sortedProducts =
                unsortedProducts.Select(p => new
                {
                    Quantity = productQuantityLookup.ContainsKey(p.Name) ? productQuantityLookup[p.Name] : 0,
                    Product = p
                }).OrderByDescending(r => r.Quantity).Select(r => r.Product).ToList();

            return sortedProducts;
        }

        private async Task<Dictionary<string, double>> GetProductQuantityLookup()
        {
            var shopperHistoryList = await _woolworthsResourceProvider.GetShopperHistory();

            var productNamesQuantityList = shopperHistoryList.SelectMany(h => h.Products)
                .GroupBy(p => p.Name)
                .Select(h => new KeyValuePair<string, double>(h.First().Name, h.Sum(r => r.Quantity))).ToList();

            var productQuantityLookup = new Dictionary<string, double>();

            foreach (var p in productNamesQuantityList)
            {
                productQuantityLookup.Add(p.Key, p.Value);
            }

            return productQuantityLookup;
        }
    }
}