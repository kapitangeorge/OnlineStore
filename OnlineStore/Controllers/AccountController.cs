using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Models;
using OnlineStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _sigInManager;
        private readonly ApplicationContext _appContext;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ApplicationContext appContext)
        {
            _appContext = appContext;
            _userManager = userManager;
            _sigInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User(model.Birthday, model.Address, model.FirstName, model.LastName, model.PhoneNumber, model.Email);
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _sigInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            string url = Request.Headers["Referer"].ToString();
            string returnUrl = new Regex(@"(.*):(\d*)").Replace(url, string.Empty);
            return View(new LoginViewModel { ReturnUrl = returnUrl});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _sigInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _sigInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> UserProfile(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var model = new ProfileUserViewModel { 
                FirstName = user.FirstName, 
                LastName = user.LastName, 
                Address = user.Address, 
                Birthday = user.Birthday, 
                Email = user.Email, 
                PhoneNumber = user.PhoneNumber,
            Id = user.Id};
            var orders = _appContext.Orders.Where(r => r.UserName == username).ToList();
            var modelOrders = new List<OrdersViewModel>();
            foreach(var order in orders) 
            {
                var ordermodel = new OrdersViewModel { OrderTime = order.OrderTime, Status = order.Status, Id = order.Id };
                var orderDetails = _appContext.OrderDetails.Where(r => r.OrderId == order.Id).ToList();
                var articles = new List<Article>();
                foreach(var orderDetail in orderDetails)
                {
                    var article = _appContext.Articles.FirstOrDefault(r => r.Id == orderDetail.ArticleId);
                    articles.Add(article);
                }
                ordermodel.Articles = articles;
                modelOrders.Add(ordermodel);
            }
            model.Orders = modelOrders;
            return View(model);
        }
    }
}
