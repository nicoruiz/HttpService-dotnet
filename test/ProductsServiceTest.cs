using Xunit;

namespace httpclientrequest_test
{
    public class ProductsServiceTest
    {
        private ProductsService _productsService = new ProductsService();

        [Fact]
        public async void AddNewProduct_ReturnsId101FromNewProductCreated()
        {
            var response = await _productsService.AddProduct("Nuevo producto test");
            Assert.Equal(101, response);
        }

        [Fact]
        public async void GetAllProducts_ReturnsAListWith30Products()
        {
            var response = await _productsService.GetAllProducts();
            Assert.Equal(30, response.Count);
        }

        [Fact]
        public async void GetProductWithId1_ReturnsProductWithId1AndTitleEqualsToIphone9()
        {
            var response = await _productsService.GetProductById(1);
            Assert.Equal(1, response.Id);
            Assert.Equal("iPhone 9", response.Title);
        }

        [Fact]
        public async void SearchProductWithQueryPhone_ReturnsAListWith4Products()
        {
            var response = await _productsService.SearchProducts("q=phone");
            Assert.Equal(4, response.Count);
        }
    }
}
