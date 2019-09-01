using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Woolworths.Assessment.Enums;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;

namespace Woolworths.Assessment.Services.SortingStrategies
{
    public class DescendingSortingStrategy : IProductSortingStrategy
    {
        public SortOption GetSortType => SortOption.Descending;

        public async Task<IEnumerable<Product>> GetSortedProducts(IEnumerable<Product> unsortedProducts)
        {
            return unsortedProducts.OrderByDescending(p => p.Name);
        }
    }
}