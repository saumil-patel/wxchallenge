using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;

namespace Woolworths.Assessment.Services
{
    [ExcludeFromCodeCoverage]
    public class MockWoolworthsResourceClient : IWoolworthsResourceClient
    {

        public async Task<string> GetProducts()
        {

            /*
             [
        {
        "name": "Test Product A",
        "price": 99.99,
        "quantity": 0
        },
        {
        "name": "Test Product B",
        "price": 101.99,
        "quantity": 0
        },
        {
        "name": "Test Product C",
        "price": 10.99,
        "quantity": 0
        },
        {
        "name": "Test Product D",
        "price": 5,
        "quantity": 0
        },
        {
        "name": "Test Product F",
        "price": 999999999999,
        "quantity": 0
        }
        ]
             */
            return @"[{""name"":""Test Product A"",""price"":99.99,""quantity"":0.0},{""name"":""Test Product B"",""price"":101.99,""quantity"":0.0},{""name"":""Test Product C"",""price"":10.99,""quantity"":0.0},{""name"":""Test Product D"",""price"":5.0,""quantity"":0.0},{""name"":""Test Product F"",""price"":999999999999.0,""quantity"":0.0}]";
            
        }

        public async Task<string> GetShopperHistory()
        {
            return @"[{""customerId"":123,""products"":[{""name"":""Test Product A"",""price"":99.99,""quantity"":3.0},{""name"":""Test Product B"",""price"":101.99,""quantity"":1.0},{""name"":""Test Product F"",""price"":999999999999.0,""quantity"":1.0}]},{""customerId"":23,""products"":[{""name"":""Test Product A"",""price"":99.99,""quantity"":2.0},{""name"":""Test Product B"",""price"":101.99,""quantity"":3.0},{""name"":""Test Product F"",""price"":999999999999.0,""quantity"":1.0}]},{""customerId"":23,""products"":[{""name"":""Test Product C"",""price"":10.99,""quantity"":2.0},{""name"":""Test Product F"",""price"":999999999999.0,""quantity"":2.0}]},{""customerId"":23,""products"":[{""name"":""Test Product A"",""price"":99.99,""quantity"":1.0},{""name"":""Test Product B"",""price"":101.99,""quantity"":1.0},{""name"":""Test Product C"",""price"":10.99,""quantity"":1.0}]}]";
        }

        public async Task<string> CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest)
        {
            /*
             //sample request which is useless in Mock setup for now
string testRequest = @"{
  ""products"": [
    {
      ""name"": ""Test Product A"",
      ""price"": 99.99
    },
    {
      ""name"": ""Test Product B"",
      ""price"": 29.99
    }
  ],
  ""specials"": [
    {
      ""quantities"": [
        {
          ""name"": ""Test Product A"",
          ""quantity"": 3
        },
        {
          ""name"": ""Test Product B"",
          ""quantity"": 3
        }
      ],
      ""total"": 6
    }
  ],
  ""quantities"": [
    {
      ""name"": ""Test Product A"",
      ""quantity"": 3
    },
    {
      ""name"": ""Test Product B"",
      ""quantity"": 3
    }
  ]
}";
             */

            return "6";
        }
    }
}