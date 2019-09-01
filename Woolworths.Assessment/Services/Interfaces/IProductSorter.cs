using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworths.Assessment.Enums;
using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public interface IProductSorter
    {
        Task<IEnumerable<Product>> GetSortedProducts(SortOption sortOption, IEnumerable<Product> unsortedProducts);
    }
}