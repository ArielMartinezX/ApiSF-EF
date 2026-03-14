using Application.Interfaces;
using Models;
using System.Net.Http.Json;

namespace Application.Proxies
{
    public class HttpSWProxy : IHttpSWProxy
    {
        private readonly HttpClient _httpClientFactory;

        public HttpSWProxy(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory.CreateClient("StarWarsApi");
        }

        public async Task<List<SWPeople>> GetAllAsync()
        {
            var response = await _httpClientFactory.GetAsync("people");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<List<SWPeople>>();
        }

        public async Task<SWPeople> GetByIdAsync(int id)
        {
            var response = await _httpClientFactory.GetAsync($"people/{id}");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<SWPeople>();
        }
    }
}
