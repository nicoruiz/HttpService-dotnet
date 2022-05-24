using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace httpclientrequest_test
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
            var response = await _httpService.SendRequestAsync("POST", "products/add", body: productBody);

            var productId = JsonConvert.DeserializeObject<Product>(response).Id;

            return productId;
        }

        public async Task<ICollection<Product>> GetAllProducts() {
            var response = await _httpService.SendRequestAsync("GET", "products");
            var jsonResponse = JObject.Parse(response);

            var products = JsonConvert.DeserializeObject<ICollection<Product>>(jsonResponse.SelectToken("products").ToString());

            return products;
        }

        public async Task<Product> GetProductById(int id) {
            var response = await _httpService.SendRequestAsync("GET", $"products/{id}");
            var product = JsonConvert.DeserializeObject<Product>(response);

            return product;
        }

        public async Task<ICollection<Product>> SearchProducts(string queryParams) {
            var response = await _httpService.SendRequestAsync("GET", "products/search", queryParams);
            var jsonResponse = JObject.Parse(response);

            var products = JsonConvert.DeserializeObject<ICollection<Product>>(jsonResponse.SelectToken("products").ToString());

            return products;
        }
    }
}