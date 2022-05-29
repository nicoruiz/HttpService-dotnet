using System.Collections.Generic;
using System.Net.Mime;
using System.Net.Http;
using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HttpService_dotnet.services
{
    public class HttpService
    {
        private static HttpClient _client = new HttpClient();
        private string _baseUrl { get; set; }

        public void SetBaseUrl(string baseUrl) {
            this._baseUrl = baseUrl;
        }

        public async Task<string> SendRequestAsync(string method, string action, Dictionary<string, string> headers = null, string queryParams = null, string body = null)
            => await InnerSendRequestAsync(method, action, headers, queryParams, body);

        public async Task<T> SendRequestAsync<T>(string method, string action, Dictionary<string, string> headers = null, string queryParams = null, string body = null)
        {
            try {
                var response = await InnerSendRequestAsync(method, action, headers, queryParams, body);
                return JsonConvert.DeserializeObject<T>(response);
            }
            catch(JsonException ex) {
                Console.WriteLine($"Error occurred during object deserialization: {ex.Message}");
                throw ex;
            }
        }

        private async Task<string> InnerSendRequestAsync(string method, string action, Dictionary<string, string> headers = null, string queryParams = null, string body = null)
        {
            try {
                if (queryParams != null && queryParams.Length > 0)
                    queryParams = $"?{queryParams}";

                var requestMessage = new HttpRequestMessage();
                requestMessage.RequestUri = new Uri(new Uri(_baseUrl), $"{action}{queryParams}");
                if (headers != null) {
                    foreach (var item in headers)
                    {
                        requestMessage.Headers.Add(item.Key, item.Value);
                    }
                }
                requestMessage.Method = new HttpMethod(method);
                requestMessage.Content = new StringContent(body != null ? body : String.Empty, Encoding.UTF8, MediaTypeNames.Application.Json);

                var response = await _client.SendAsync(requestMessage);
                var data = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"HTTP Status Response: {response.StatusCode.ToString()}");
                Console.WriteLine($"Response data: {data}");
                
                return data;
            }
            catch(System.Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                throw ex;
            }
        }
    }
}