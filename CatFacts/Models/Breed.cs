using SQLite;

namespace CatFacts.Models
{
    public class Breed
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BreedName { get; set; }
        public string Country { get; set; }
        public string Origin { get; set; }
        public string Coat { get; set; }
        public string Pattern { get; set; }

        public Breed() { }

        public Breed(string breedName, string country, string origin, string coat, string pattern)
        {
            BreedName = breedName;
            Country = country;
            Origin = origin;
            Coat = coat;
            Pattern = pattern;
        }
    }
}