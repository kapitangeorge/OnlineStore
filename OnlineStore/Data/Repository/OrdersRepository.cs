using OnlineStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repository
{
    public class OrdersRepository
    {
        private readonly ApplicationContext _appContext;
        private readonly ShopCart _shopCart;

        public OrdersRepository (ApplicationContext appContex, ShopCart shopCart)
        {
            _appContext = appContex;
            _shopCart = shopCart;
        }
        public void CreateOrder(Order order)
        {
            order.OrderTime = DateTime.Now;
            order.Status = "В обработке";
            _appContext.Orders.Add(order);
            _appContext.SaveChanges();

            var items = _shopCart.ListShopItems;

            foreach(var item in items)
            {
                var article = _appContext.Articles.FirstOrDefault(r => r.Id == item.ArticleId);
                article.Amount--;
                _appContext.Update(article);
                var orderDetail = new OrderDetail { ArticleId = item.ArticleId, OrderId = order.Id, Price = article.Price};
                _appContext.Add(orderDetail);
            }
            _appContext.SaveChanges();
        }
    }
}
