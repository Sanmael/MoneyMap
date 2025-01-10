using Integrations.DTO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Integrations.Client
{
    public class AppClient(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient _httpClient = httpClientFactory.CreateClient();

        public async Task<T?> SendRequestAsync<T>(ClientDTO<T> clientDTO)
        {            
            string content = JsonSerializer.Serialize(clientDTO);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                Content = new StringContent(content)            ,
                RequestUri = new Uri(clientDTO.Url)
            };

            HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);

            if (!httpResponseMessage.IsSuccessStatusCode)
                return default;

            return await httpResponseMessage.Content.ReadFromJsonAsync<T>();
        }

        public async Task<T?> GetAsync<T>(ClientDTO clientDTO)
        {
            Uri uri = new Uri(clientDTO.Url);

            HttpResponseMessage httpResponseMessage = await _httpClient.GetAsync(uri);

            if (!httpResponseMessage.IsSuccessStatusCode)
                return default;

            return await httpResponseMessage.Content.ReadFromJsonAsync<T>();
        }
    }
}