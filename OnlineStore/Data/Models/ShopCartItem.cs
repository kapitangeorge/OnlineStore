using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Models
{
    public class ShopCartItem
    {
        public int Id { get; set; }

        public int ArticleId { get; set; }
        
        public string ShoprCartId { get; set; }
    }
}
