using System.Net.Http.Json;
using CatFacts.Models;

namespace CatFacts.Services
{

    public class CatFactService : ICatFactService
    {
        private readonly HttpClient _httpClient;

        public CatFactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://catfact.ninja/fact");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<CatFact> GetCatFactAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadFromJsonAsync<CatFactApiResponse>();
            return new CatFact(json?.fact ?? "", json?.length ?? 0);
        }
    }

    public class CatFactApiResponse
    {
        public string fact { get; set; }
        public int length { get; set; }
    }
}