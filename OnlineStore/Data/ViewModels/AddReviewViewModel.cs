using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.ViewModels
{
    public class AddReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name="Оценка")]
        [Range(0.0,5.01)]
        public int Rating { get; set; }
        [Required]
        [Display(Name="Отзыв")]
        public string Description { get; set; }

        public string Author { get; set; }

        public int ArticleId { get; set; }
    }
}
