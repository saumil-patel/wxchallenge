using Newtonsoft.Json;

namespace Woolworths.Assessment.Models
{
    public class Quantity
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("quantity")]
        public int Number { get; set; }
    }
}