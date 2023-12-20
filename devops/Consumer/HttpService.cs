using Consumer.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Consumer
{
    public interface IHttpService
    {
        Task<int> Send(string url);
        Task UpdateLink(StatusUpdateRequest statusUpdateRequest);
    }
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        
        public HttpService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<int> Send(string url)
        {
            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            webRequest.Method = "HEAD"; // Используем метод HEAD для проверки только заголовков

            try
            {
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
                // Получаем статус код HTTP-ответа
                int statusCode = (int)webResponse.StatusCode;
                return statusCode;
            }
            catch (System.Net.WebException ex)
            {
                if (ex.Response != null)
                {
                    var response = (System.Net.HttpWebResponse)ex.Response;
                    int statusCode = (int)response.StatusCode;
                    return statusCode;
                }
                else
                {
                    return 500;
                }

            }

        }

        public async Task UpdateLink(StatusUpdateRequest statusUpdateRequest)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Patch, "http://lab-nginx/Links");

                request.Content = new StringContent(
                    JsonConvert.SerializeObject(statusUpdateRequest), Encoding.UTF8, "application/json"
                );

                var response = await _httpClient.SendAsync(request);

                Console.WriteLine($"response.IsSuccessStatusCode {response.IsSuccessStatusCode}");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);

            }

        }
    }
}
