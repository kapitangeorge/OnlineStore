using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Models
{
    public class Review
    {
        public int Id { get; set; }

        public int Rating { get; set; }

        public string Decsription { get; set; }

        public string Author { get; set; }

        public int ArticleId { get; set; }
    }
}
