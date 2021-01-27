using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Models;
using OnlineStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly ApplicationContext _appContext;
        private readonly ShopCart _shopCart;

        public ShopCartController(ApplicationContext appContetx, ShopCart shopCart)
        {
            _appContext = appContetx;
            _shopCart = shopCart;
        }

        public IActionResult Index()
        {
            var items = _shopCart.GetShopItems();
            _shopCart.ListShopItems = items;
            var articles = new List<Article>();
            uint sum = 0;
            foreach(var el in _shopCart.ListShopItems)
            {
                var article = _appContext.Articles.FirstOrDefault(r => r.Id == el.ArticleId);
                articles.Add(article);
                sum += article.Price;
            }
            var model = new ShopCartViewModel { ShopCart = _shopCart, Articles = articles, Sum = sum};

            return View(model);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var article = await _appContext.Articles.FirstOrDefaultAsync(r => r.Id == id);
            if(article != null)
            {
                _shopCart.AddToCart(id);
            }

            return RedirectToAction("Index");
        }
    }
}
