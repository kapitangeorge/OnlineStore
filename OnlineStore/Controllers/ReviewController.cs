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
    public class ReviewController : Controller
    {
        private readonly ApplicationContext _appContext;

        public ReviewController(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public IActionResult Create(int id)
        {
            var model = new AddReviewViewModel { ArticleId = id };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create (AddReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = await _appContext.Articles.SingleOrDefaultAsync(r => r.Id == model.ArticleId);
                
                Review review = new Review { ArticleId = model.ArticleId, Rating = model.Rating, Decsription = model.Description, Author = model.Author };

                _appContext.Reviews.Add(review);
                await _appContext.SaveChangesAsync();

                var reviews = _appContext.Reviews.Where(r => r.ArticleId == article.Id).ToList();

                article.Rating = reviews.Sum(r => r.Rating) / reviews.Count;

                _appContext.Update(article);
                await _appContext.SaveChangesAsync();

                return RedirectToAction("ArticleProfile", "Articles", new { id = article.Id });
            }
            return View(model);
        }

       [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            var review = await _appContext.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if(review != null)
            {
                var model = new EditReviewViewModel { Rating = review.Rating, Description = review.Decsription, Id = id };
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit (EditReviewViewModel model)
        {
            if (ModelState.IsValid)
            {
                var review = await _appContext.Reviews.FirstOrDefaultAsync(r => r.Id == model.Id);
                review.Decsription = model.Description;
                review.Rating = model.Rating;
                _appContext.Update(review);
                await _appContext.SaveChangesAsync();

                return RedirectToAction("ArticleProfile", "Articles", new { id = review.ArticleId });
            }

            return View(model);
            
        }
    }

}
