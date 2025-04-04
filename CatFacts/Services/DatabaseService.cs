using CatFacts.Models;
using SQLite;

namespace CatFacts.Services
{

    public class DatabaseService : IDatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Breed>().Wait();
            _database.CreateTableAsync<CatFact>().Wait();
            _database.CreateTableAsync<Cat>().Wait();
        }

        public Task<List<Breed>> GetBreedsAsync()
        {
            return _database.Table<Breed>().ToListAsync();
        }

        public Task<int> SaveBreedAsync(Breed breed)
        {
            return breed.Id != 0 ? _database.UpdateAsync(breed) : _database.InsertAsync(breed);
        }

        public Task<int> DeleteBreedAsync(Breed breed)
        {
            return _database.DeleteAsync(breed);
        }

        public Task<List<CatFact>> GetCatFactsAsync()
        {
            return _database.Table<CatFact>().ToListAsync();
        }

        public Task<int> SaveCatFactAsync(CatFact catFact)
        {
            return catFact.Id != 0 ? _database.UpdateAsync(catFact) : _database.InsertAsync(catFact);
        }

        public Task<int> DeleteCatFactAsync(CatFact catFact)
        {
            return _database.DeleteAsync(catFact);
        }


        public Task<int> SaveCatAsync(Cat cat) => cat.Id != 0 ? _database.UpdateAsync(cat) : _database.InsertAsync(cat);
        public Task<List<Cat>> GetCatsAsync() => _database.Table<Cat>().ToListAsync();
        public Task<int> DeleteCatAsync(Cat cat) => _database.DeleteAsync(cat);

    }
}