using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatFacts.Models;

namespace CatFacts.Services
{
    public interface IBreedService
    {
        Task<List<Breed>> GetBreedsAsync(); 
    }
}
