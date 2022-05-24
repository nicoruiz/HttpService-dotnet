using System.Net.Mime;
using System.Net.Http;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace httpclientrequest_test
{
    public class HttpService
    {
        private string _baseUrl { get; set; }

        public HttpService(string baseUrl) 
        {
            this._baseUrl = baseUrl;
        }

        public async Task<string> SendRequestAsync(string method, string action, string queryParams = null, string body = null)
        {
            try {
                if (queryParams != null && queryParams.Length > 0)
                    queryParams = $"?{queryParams}";

                using (var client = new HttpClient()) 
                {
                    client.BaseAddress = new Uri(_baseUrl);

                    var requestMessage = new HttpRequestMessage();
                    requestMessage.RequestUri = new Uri(client.BaseAddress, $"{action}{queryParams}");
                    requestMessage.Method = new HttpMethod(method);
                    requestMessage.Content = new StringContent(body != null ? body : String.Empty, Encoding.UTF8, MediaTypeNames.Application.Json);

                    var response = await client.SendAsync(requestMessage);
                    var data = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"HTTP Status Response: {response.StatusCode.ToString()}");
                    Console.WriteLine($"Response data: {data}");

                    return data;
                }
            }
            catch(System.Exception ex)
            {
                Console.WriteLine($"Error occured: {ex.Message}");
                throw ex;
            }
        }
    }
}