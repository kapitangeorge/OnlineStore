using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Models
{
    public class ShopCart
    {
        private readonly ApplicationContext _appContext;

        public ShopCart(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public string ShopCartId { get; set; }

        public List<ShopCartItem> ListShopItems { get; set; }


        public static ShopCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<ApplicationContext>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId };
        }

        public void AddToCart(int id)
        {
            _appContext.ShopCartItems.Add(new ShopCartItem { ShoprCartId = ShopCartId, ArticleId = id });
            _appContext.SaveChanges();
        }

        public List<ShopCartItem> GetShopItems()
        {
            return _appContext.ShopCartItems.Where(r => r.ShoprCartId == ShopCartId).ToList();
        }
    }
}
