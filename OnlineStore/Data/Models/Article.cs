using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Models
{
    public class Article
    {
        public Article(string name, uint price, string description, float rating, uint amount, bool isFavorite)
        {
            Name = name;
            Price = price;
            Description = description;
            Rating = rating;
            Amount = amount;
            IsFavorite = isFavorite;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public uint Price { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public uint Amount { get; set; }

        public bool IsFavorite { get; set; }                                          

        public List<string> Images { get; set; }
    }
}
