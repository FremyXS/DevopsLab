using Microsoft.Extensions.Options;
using System.Net;
using System.Text;

namespace Laborotor.Services.Http
{
    public interface IHttpService
    {
        Task<int> Send(string url);
    }
    public class HttpService: IHttpService
    {

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
    }
}
