using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Woolworths.Assessment.Models;
using Woolworths.Assessment.Services.Interfaces;

namespace Woolworths.Assessment.Services
{
    public class WoolworthsResourceClient : IWoolworthsResourceClient
    {
        private readonly HttpClient _client;
        private Uri ProductsUri { get; }
        private Uri ShopperHistoryUri { get; }
        private Uri TrolleyCalculatorUri { get; }
        
        private readonly ILogger<WoolworthsResourceClient> _logger;

        public WoolworthsResourceClient(AppSettings appSettings, ILogger<WoolworthsResourceClient> logger)
        {
            _logger = logger;
            
            var queryBuilder = new QueryBuilder { { "Token", appSettings.Token() } };
            var baseUri = new Uri(appSettings.WoolworthsResourceUrl());

            ProductsUri = GetResourceUri(baseUri, "products", queryBuilder);
            ShopperHistoryUri = GetResourceUri(baseUri, "shopperHistory", queryBuilder);
            TrolleyCalculatorUri = GetResourceUri(baseUri, "trolleyCalculator", queryBuilder);
            
            
            
            _client = new HttpClient(); //the unique disposable not to be disposed!
        }

        private static Uri GetResourceUri(Uri baseUri, string relativeUri, QueryBuilder queryBuilder)
        {
            var uriBuilder = new UriBuilder(new Uri(baseUri, relativeUri)) {Query = queryBuilder.ToString()};
            var resourceUri = uriBuilder.Uri;
            return resourceUri;
        }


        public async Task<string> GetProducts()
        {
            return await GetJsonResultFromUri(ProductsUri);
        }

        public async Task<string> GetShopperHistory()
        {
            return await GetJsonResultFromUri(ShopperHistoryUri);
        }

        public async Task<string> CalculateTrolleyTotal(TrolleyTotalRequest trolleyTotalRequest)
        {
            return await postToUri(TrolleyCalculatorUri, trolleyTotalRequest);
        }

        private async Task<string> GetJsonResultFromUri(Uri uri)
        {
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.GetAsync(uri);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return content;

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var errorMsg =
                $"Error getting from {uri} ; Status Code: {response.StatusCode} ; Error message: {response.ReasonPhrase}; Content {content}";
            _logger.LogError(errorMsg);
            throw new ApplicationException(errorMsg);

        }

        private async Task<string> postToUri<T>(Uri uri, T payload)
        {

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.PostAsJsonAsync(uri, payload);

            var content = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
                return content;

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var errorMsg =
                $"Error getting from {uri} ; Status Code: {response.StatusCode} ; Error message: {response.ReasonPhrase}; Content {content}";
            _logger.LogError(errorMsg);
            throw new ApplicationException(errorMsg);

        }
    }
}