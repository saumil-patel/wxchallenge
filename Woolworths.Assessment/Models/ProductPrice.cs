﻿using Newtonsoft.Json;

namespace Woolworths.Assessment.Models
{
    public class ProductPrice
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("price")]
        public decimal Price { get; set; }
    }
}