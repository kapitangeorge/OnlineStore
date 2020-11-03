using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Models;
using OnlineStore.Data.Repository;
using OnlineStore.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly ApplicationContext _appContext;
        IWebHostEnvironment _appEnvironment;

        public ArticlesController (IWebHostEnvironment appEnvironment, ApplicationContext appContext)
        {
            _appEnvironment = appEnvironment;
            _appContext = appContext;
        }
        public IActionResult AllArticles()
        {
            return View(_appContext.Articles.OrderBy(r => r.Name).ToList());
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create (CreateArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var article = new Article(model.Name, model.Price, model.Description, 0, model.Amount, model.IsFavorite);

                if (model.Files != null)
                {
                    article.Images = SaveFile(model.Files);
                }

                _appContext.Articles.Add(article);
                await _appContext.SaveChangesAsync();

                return RedirectToAction("AllArticles");
            }

            return View(model);
        }

        private List<string> SaveFile(List<IFormFile> files)
        {
            var images = new List<string>();
            foreach (var file in files)
            {
                string path = "/img/" + file.FileName;

                using (var filestream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                images.Add(path);
            }

            return images;
        }

        public IActionResult ArticleProfile(int id)
        {
            var article = _appContext.Articles.FirstOrDefault(r => r.Id == id);
            if(article != null)
            {
                return View(article);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Edit(int id)
        {
            var article = _appContext.Articles.FirstOrDefault(r => r.Id == id);
            if (article != null)
            {
                var model = new EditArticleViewModel  
                {
                    Name = article.Name,
                    Price = article.Price,
                    Description = article.Description,
                    Amount = article.Amount,
                    IsFavorite = article.IsFavorite,
                    Images = article.Images,
                    Id = article.Id,
                }
            ;
                return View(model);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit (EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                Article article = await _appContext.Articles.SingleOrDefaultAsync(r => r.Id == model.Id);
                if(article != null)
                {
                    article.Name = model.Name;
                    article.Price = model.Price;
                    article.Description = model.Description;
                    article.Amount = model.Amount;
                    article.IsFavorite = model.IsFavorite;

                    if (model.Files != null)
                    {
                        if(article.Images != null)
                        {
                            article.Images.AddRange(SaveFile(model.Files));
                        }
                        else
                        {
                            article.Images = SaveFile(model.Files);
                        }
                    }
                    _appContext.Update(article);
                    await _appContext.SaveChangesAsync();
                    return RedirectToAction("ArticleProfile", "Articles", new { id = model.Id });
                }
                else
                {
                    return NotFound();
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage(string path, int id)
        {
            var article = await _appContext.Articles.SingleOrDefaultAsync(r => r.Id == id);
            RemoveFileFromServer(path);
            article.Images.Remove(path);
            _appContext.Update(article);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Edit", "Articles", new { id = id });

        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle (int id)
        {
            var article = await _appContext.Articles.SingleOrDefaultAsync(r => r.Id == id);
            foreach(var path in article.Images)
            {
                RemoveFileFromServer(path);
            }
            _appContext.Remove(article);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("AllArticles");
        }

        private bool RemoveFileFromServer(string path)
        {
            string fullPath = _appEnvironment.WebRootPath + path;
            if (!System.IO.File.Exists(fullPath)) return false;
            try
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            catch (Exception e)
            {
                //Debug.WriteLine(e.Message);
            }
            return false;
        }
    }

    
}
