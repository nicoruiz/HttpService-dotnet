using System.Collections.Generic;
namespace HttpService_dotnet.models
{
    public class ProductsApiResponse
    {
        public ICollection<Product> Products { get; set; }
        public int Total { get; set; }
        public int Skip { get; set; }
        public int Limit { get; set; }
    }
}