using OnlineStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.ViewModels
{
    public class ShopCartViewModel
    {
        public ShopCart ShopCart { get; set; }

        public List<Article> Articles { get; set; }

        public uint Sum { get; set; }
    }
}
