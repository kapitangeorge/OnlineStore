using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.ViewModels
{
    public class ProfileUserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        
        public DateTime Birthday { get; set; }

        public string Address { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public List<OrdersViewModel> Orders { get; set; }
    }
}
