using System.Net.Http.Json;
using CatFacts.DTOs;
using CatFacts.Models;

namespace CatFacts.Services
{ 
    public class BreedService : IBreedService
    {
        private readonly HttpClient _httpClient;

        public BreedService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://catfact.ninja/breeds");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Breed>> GetBreedsAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            var breedResponse = await response.Content.ReadFromJsonAsync<BreedApiResponse>();
            return breedResponse?.data.Select(b => new Breed(b.breed, b.country, b.origin, b.coat, b.pattern)).ToList() ?? new List<Breed>();
        }
    }

}