using OnlineStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.ViewModels
{
    public class OrdersViewModel
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }

        public string Status { get; set; }

        public List<Article> Articles { get; set; }

        public User User { get; set; }
    }
}
