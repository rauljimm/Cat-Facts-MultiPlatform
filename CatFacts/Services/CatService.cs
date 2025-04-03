using System.Threading.Tasks;
using CatFacts.Models;

namespace CatFacts.Services
{
    public class CatService : ICatService
    {
        private readonly IDatabaseService _databaseService;

        public CatService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<Cat> CreateCatAsync(string name, string breed, string color, int age)
        {
            var cat = new Cat(name, breed, color, age);
            await _databaseService.SaveCatAsync(cat); 
            return cat; 
        }

        public async Task<List<Cat>> GetCatsAsync()
        {
            return await _databaseService.GetCatsAsync();
        }

        public async Task DeleteCatAsync(Cat cat)
        {
            await _databaseService.DeleteCatAsync(cat);
        }

    }
}
