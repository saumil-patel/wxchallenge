using Newtonsoft.Json;

namespace Woolworths.Assessment.Models
{
    public class TrolleyTotalRequest
    {
        [JsonProperty("products")]
        public ProductPrice[] Products { get; set; }

        [JsonProperty("specials")]
        public Special[] Specials { get; set; }

        [JsonProperty("quantities")]
        public Quantity[] Quantities { get; set; }
    }
}