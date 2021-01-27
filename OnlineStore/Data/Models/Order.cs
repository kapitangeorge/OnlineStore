using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderTime { get; set; }

        public string Status { get; set; }

        public string UserName { get; set; }

    }
}
