using Microsoft.EntityFrameworkCore;
using OnlineStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repository
{
    public class ArticlesRepository
    {
        private readonly ApplicationContext appContext;

        public ArticlesRepository (ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public List<Article> GetFavArticles => appContext.Articles.Where(r => r.IsFavorite == true).ToList();

        public List<Article> AllArticles => appContext.Articles.Include(r => r.Id).ToList();

        public void AddArticle (Article article)
        {
            appContext.Articles.Add(article);
            appContext.SaveChanges();
        }
    }
}
