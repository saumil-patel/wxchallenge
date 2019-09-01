using System.Collections.Generic;
using System.Threading.Tasks;
using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public interface IWoolworthsResourceProvider
    {
        Task<List<Product>> GetProducts();
        Task<List<ShopperHistory>> GetShopperHistory();
        Task<double> CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest);
    }
}