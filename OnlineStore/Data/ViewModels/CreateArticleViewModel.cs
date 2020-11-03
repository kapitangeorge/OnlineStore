using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.ViewModels
{
    public class CreateArticleViewModel
    {
        [Required]
        [Display(Name = "Наименование товара")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Цена за единицу")]
        public uint Price { get; set; }

        [Required]
        [Display(Name = "Описание товара")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Количество товара")]
        public uint Amount { get; set; }

        [Required]
        [Display(Name="Избраное")]
        public bool IsFavorite { get; set; }

        public List<IFormFile> Files { get; set; }
    }
}
