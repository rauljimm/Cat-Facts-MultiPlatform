using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace CatFacts.Models
{
    public class Cat
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public string Color { get; set; }
        public int Age { get; set; }

        public Cat(string name, string breed, string color, int age)
        {
            Name = name;
            Breed = breed;
            Color = color;
            Age = age;
        }
    }
}
