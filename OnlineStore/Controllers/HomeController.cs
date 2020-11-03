using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Models;
using OnlineStore.Data.Repository;
using OnlineStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _appContext;
        
        public HomeController(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var favArticles = _appContext.Articles.Where(r => r.IsFavorite == true).ToList();
            if (favArticles != null)
            {
                return View(favArticles);
            }
            return View();
        }

       
    }
}
