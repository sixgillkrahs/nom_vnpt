using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CSP_NET.Work.SyncCtin
{
    public class BaseHttpClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _nameServiceRemove;
        protected BaseHttpClient(IHttpClientFactory httpClientFactory, string nameServiceRemove)
        {
            _httpClientFactory = httpClientFactory;
            _nameServiceRemove = nameServiceRemove;
        }

        protected async Task<T?> GetAsync<T>(string url)
        {
            using (var client = _httpClientFactory.CreateClient(_nameServiceRemove))
            {
                return await client.GetFromJsonAsync<T>(url, new JsonSerializerOptions()
                {
        //            PropertyNameCaseInsensitive = true
                });
            }
        }

        protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string url, TRequest requestContent)
        {
            var client = _httpClientFactory.CreateClient(_nameServiceRemove);
            StringContent httpContent = null;
            if (requestContent != null)
            {
                var json = JsonConvert.SerializeObject(requestContent, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await client.PostAsync(url, httpContent);

            var body = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TResponse>(body, new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
            }
            throw new Exception(body);
        }

        protected async Task<TResponse?> PutAsync<TRequest, TResponse>(string url, TRequest requestContent)
        {
            var client = _httpClientFactory.CreateClient(_nameServiceRemove);
            HttpContent httpContent = null;
            if (requestContent != null)
            {
                var json = JsonSerializer.Serialize(requestContent);
                httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            }
            var response = await client.PutAsJsonAsync(url, httpContent);
            var body = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponse>(body, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

        }
        protected async Task<bool> DeleteAsync(string url)
        {
            var client = _httpClientFactory.CreateClient(_nameServiceRemove);
            var response = await client.DeleteAsync(url);
            if (!response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                throw new Exception("Delete Error");

            var result = response.IsSuccessStatusCode;
            return result;
        }
    }
}
