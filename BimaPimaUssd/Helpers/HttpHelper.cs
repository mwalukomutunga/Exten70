using BimaPimaUssd.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BimaPimaUssd.Helpers
{
    public class HttpHelper
    {
        private readonly HttpClient client;
        public HttpHelper(HttpClient client)
        {
            this.client = client;
        }
        public async Task<object> ProcessGetRequest<T>(string url)
        {
            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var newObject = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(json) : default;
            var error = !response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync()) : default;
            return newObject is null ? error : newObject;
        }
        public async Task<dynamic> ProcessPostRequest<T, U>(string url, U payload)
        {
            var json = JsonConvert.SerializeObject(payload);
            var data = new StringContent(json, Encoding.UTF8, AppConstant.Header);
            var response = await client.PostAsync(url, data);
            var res = await response.Content.ReadAsStringAsync();
            var newObject = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(res) : default;
            var error = !response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync()) : default;
            return newObject is null ? error : newObject;
        }
        public async Task<object> ProcessGetRequest<T>(string token, string url)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue(AppConstant.Header));

            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var newObject = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(json) : default;
            var error = !response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync()) : default;
            return newObject is null ? error : newObject;
        }
        public async Task<dynamic> ProcessPostRequest<T, U>(string token, string url, U payload)
        {
            client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders
             .Accept
             .Add(new MediaTypeWithQualityHeaderValue(AppConstant.Header));
            var json = JsonConvert.SerializeObject(payload);
            var data = new StringContent(json, Encoding.UTF8, AppConstant.Header);
            var response = await client.PostAsync(url, data);
            var newObject = response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) : default;
            var error = !response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<Error>(await response.Content.ReadAsStringAsync()) : default;
            return newObject is null ? error : newObject;

        }
    }
}
