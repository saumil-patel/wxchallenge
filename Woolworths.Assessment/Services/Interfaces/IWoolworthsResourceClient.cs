using System.Threading.Tasks;
using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{ 
    public interface IWoolworthsResourceClient
    {
        Task<string> GetProducts();
        Task<string> GetShopperHistory();
        Task<string> CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest);
        
    }
}