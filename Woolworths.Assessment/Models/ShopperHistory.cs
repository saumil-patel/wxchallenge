using Newtonsoft.Json;

namespace Woolworths.Assessment.Models
{
    public class ShopperHistory
    {
        [JsonProperty("customerId")]
        public int CustomerId { get; set; }

        [JsonProperty("products")]
        public Product[] Products { get; set; }
    }
}