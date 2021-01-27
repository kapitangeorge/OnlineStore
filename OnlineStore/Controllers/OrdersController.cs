using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Models;
using OnlineStore.Data.Repository;
using OnlineStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationContext _appContext;
        private readonly ShopCart _shopCart;

        public OrdersController(ApplicationContext appContext, ShopCart shopCart)
        {
            _appContext = appContext;
            _shopCart = shopCart;
        }

        [HttpGet]
        public IActionResult AllOrders()
        {
            var model = new List<OrdersViewModel>();
            var orders = _appContext.Orders.ToList().OrderByDescending(r => r.OrderTime);
            foreach (var order in orders)
            {
                var orderViewModel = new OrdersViewModel { OrderTime = order.OrderTime, Status = order.Status, Id = order.Id};
                var orderDetails = _appContext.OrderDetails.Where(r => r.OrderId == order.Id).ToList();
                var articles = new List<Article>();
                foreach(var el in orderDetails)
                {
                    articles.Add(_appContext.Articles.FirstOrDefault(r => r.Id == el.ArticleId));
                }
                orderViewModel.Articles = articles;
                orderViewModel.User = _appContext.Users.FirstOrDefault(r => r.Email == order.UserName);
                model.Add(orderViewModel);
            }

            return View(model);
        } 

       public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult AddOrder(string username)
        {
            _shopCart.ListShopItems = _shopCart.GetShopItems();

            if(_shopCart.ListShopItems.Count == 0)
            {
                return RedirectToAction("Index", "ShopCart");
            }
            CreateOrder(username);

            return RedirectToAction("Index");
        } 

        [HttpPost]
        public async Task<IActionResult> ChangeStatusSend(int orderId)
        {
            var order =  _appContext.Orders.FirstOrDefault(r => r.Id == orderId);
            order.Status = "Отправлен";
             _appContext.Update(order);
           await  _appContext.SaveChangesAsync();

            return RedirectToAction("AllOrders");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatusReceived(int orderId)
        {
            var order = _appContext.Orders.FirstOrDefault(r => r.Id == orderId);
            order.Status = "Получен";
            _appContext.Update(order);
            await _appContext.SaveChangesAsync();

            return RedirectToAction("UserProfile", "Account", new { username = order.UserName});
        }
        private void CreateOrder(string username)
        {
            var order = new Order { UserName = username };
            order.OrderTime = DateTime.Now;
            order.Status = "В обработке";
            _appContext.Orders.Add(order);
            _appContext.SaveChanges();

            var items = _shopCart.ListShopItems;

            foreach (var item in items)
            {
                var article = _appContext.Articles.FirstOrDefault(r => r.Id == item.ArticleId);
                article.Amount--;
                _appContext.Update(article);
                var orderDetail = new OrderDetail { ArticleId = item.ArticleId, OrderId = order.Id, Price = article.Price };
                _appContext.Add(orderDetail);
            }
            _appContext.SaveChanges();

        }
    }
}
