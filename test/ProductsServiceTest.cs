using Xunit;
using HttpService_dotnet.services;

namespace httpclientrequest_test
{
    public class ProductsServiceTest
    {
        // Simulates DI Container.
        private static HttpService _httpService = new HttpService();
        private ProductsService _productsService = new ProductsService(_httpService);

        [Fact]
        public async void AddNewProduct_ReturnsId101FromNewProductCreated()
        {
            var response = await _productsService.AddProduct("Nuevo producto test");
            Assert.Equal(101, response);
        }

        [Fact]
        public async void GetAllProducts_Returns100Products()
        {
            var response = await _productsService.GetAllProducts();
            Assert.Equal(100, response.Total);
        }

        [Fact]
        public async void GetProductWithId1_ReturnsProductWithId1AndTitleEqualsToIphone9()
        {
            var response = await _productsService.GetProductById(1);
            Assert.Equal(1, response.Id);
            Assert.Equal("iPhone 9", response.Title);
        }

        [Fact]
        public async void SearchProductWithQueryPhone_Returns4Products()
        {
            var response = await _productsService.SearchProducts("q=phone");
            Assert.Equal(4, response.Total);
        }
    }
}
