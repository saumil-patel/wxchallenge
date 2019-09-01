using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Woolworths.Assessment.Models;

namespace Woolworths.Assessment.Services.Interfaces
{
    public class WoolworthsResourceProvider : IWoolworthsResourceProvider
    {
        private readonly IWoolworthsResourceClient _woolworthsResourceClient;

        public WoolworthsResourceProvider(IWoolworthsResourceClient woolworthsResourceClient)
        {
            _woolworthsResourceClient = woolworthsResourceClient;
        }

        public async Task<List<Product>> GetProducts()
        {
            var products = await _woolworthsResourceClient.GetProducts();
            return JsonConvert.DeserializeObject<List<Product>>(products);
        }

        public async Task<List<ShopperHistory>> GetShopperHistory()
        {
            var shopperHistory = await _woolworthsResourceClient.GetShopperHistory();
            return JsonConvert.DeserializeObject<List<ShopperHistory>>(shopperHistory);
        }

        public async Task<double> CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest)
        {
            var result = await _woolworthsResourceClient.CalculateTrolleyTotal(trolleyTotalRequest);
            return double.Parse(result);
        }
    }
}