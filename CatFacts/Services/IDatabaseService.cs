using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatFacts.Models;

namespace CatFacts.Services
{
    public interface IDatabaseService
    {
        Task<List<Breed>> GetBreedsAsync();
        Task<int> SaveBreedAsync(Breed breed);
        Task<int> DeleteBreedAsync(Breed breed);
        Task<List<CatFact>> GetCatFactsAsync();
        Task<int> SaveCatFactAsync(CatFact catFact);
        Task<int> DeleteCatFactAsync(CatFact catFact);
    }
}
