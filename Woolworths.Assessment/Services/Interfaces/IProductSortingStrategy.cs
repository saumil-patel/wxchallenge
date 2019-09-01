using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworths.Assessment.Enums;
using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public interface IProductSortingStrategy
    {
        SortOption GetSortType { get; }
        Task<IEnumerable<Product>> GetSortedProducts(IEnumerable<Product> unsortedProducts);
    }
}