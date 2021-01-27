using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ArticleId { get; set; }

        public uint Price { get; set; }

        public virtual Article Article { get; set; }

        public virtual Order Order { get; set; }
    }
}
