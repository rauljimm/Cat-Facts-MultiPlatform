using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatFacts.Models;

namespace CatFacts.Services
{
    public interface ICatService
    {
        Task<Cat> CreateCatAsync(string name, string breed, string color, int age);
        Task DeleteCatAsync(Cat cat);
        Task<List<Cat>> GetCatsAsync();


    }
}
