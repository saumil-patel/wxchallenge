using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworths.Assessment.Enums;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;

namespace Woolworths.Assessment.Services
{
    public class ProductSorter : IProductSorter
    {
        private readonly Dictionary<SortOption, IProductSortingStrategy> _productSortingStrategies;

        public ProductSorter(IEnumerable<IProductSortingStrategy> productSortingStrategies)
        {
            _productSortingStrategies = new Dictionary<SortOption, IProductSortingStrategy>();

            foreach (var productSortingStrategy in productSortingStrategies)
            {
                _productSortingStrategies.Add(productSortingStrategy.GetSortType, productSortingStrategy);
            }
        }

        public async Task<IEnumerable<Product>> GetSortedProducts(SortOption sortOption, IEnumerable<Product> unsortedProducts)
        {
            return await _productSortingStrategies[sortOption].GetSortedProducts(unsortedProducts);
        }
    }
}