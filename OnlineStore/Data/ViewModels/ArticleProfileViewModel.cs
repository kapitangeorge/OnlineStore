using OnlineStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.ViewModels
{
    public class ArticleProfileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public uint Price { get; set; }

        public string Description { get; set; }

        public float Rating { get; set; }

        public uint Amount { get; set; }

        public bool IsFavorite { get; set; }
        public List<Review> Reviews { get; set; }

        public List<string> Images { get; set; }
    }
}
