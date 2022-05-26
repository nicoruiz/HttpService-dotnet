using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HttpService_dotnet.models;

namespace HttpService_dotnet.services
{
    public class ProductsService
    {
        private const string BASE_URL = "https://dummyjson.com/";
        private HttpService _httpService { get; set; }

        public ProductsService() 
        {
            this._httpService = new HttpService(BASE_URL);
        }

        public async Task<int> AddProduct(string productName)
        {
            var product = new Product { Title = productName };
            var productBody = JsonConvert.SerializeObject(product);
            var headers = new Dictionary<string, string>() { { "Accept", "application/json" } }; // No headers needed, added just for testing purposes.

            var response = await _httpService.SendRequestAsync<Product>("POST", "products/add", headers: headers, body: productBody);

            return response.Id;
        }

        public async Task<ProductsApiResponse> GetAllProducts() {
            var response = await _httpService.SendRequestAsync<ProductsApiResponse>("GET", "products");

            return response;
        }

        public async Task<Product> GetProductById(int id) {
            var response = await _httpService.SendRequestAsync<Product>("GET", $"products/{id}");

            return response;
        }

        public async Task<ProductsApiResponse> SearchProducts(string queryParams) {
            var response = await _httpService.SendRequestAsync<ProductsApiResponse>("GET", "products/search", queryParams: queryParams);

            return response;
        }
    }
}