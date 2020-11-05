using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.ViewModels
{
    public class EditReviewViewModel
    {
        public int Id { get; set; }
        public int Rating { get; set; }

        public string Description { get; set; }
    }
}
