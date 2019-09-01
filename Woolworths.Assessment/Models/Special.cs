using Newtonsoft.Json;

namespace Woolworths.Assessment.Models
{
    public class Special
    {
        [JsonProperty("quantities")]
        public Quantity[] Quantities { get; set; }

        [JsonProperty("total")]
        public double Total { get; set; }
    }
}