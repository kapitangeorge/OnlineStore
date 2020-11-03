using OnlineStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Data.Repository
{
    public class ReviewsRepository
    {
        private readonly ApplicationContext appContext;

        public ReviewsRepository(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public List<Review> GetAllReviewsForArticle (int articleId)
        {
            return appContext.Reviews.Where(r => r.Id == articleId).ToList();
        }

        public float GetAverageRatingArticle (int articleId)
        {
            var reviews = GetAllReviewsForArticle(articleId);
            int sum = 0;
            foreach (var review in reviews)
            {
                sum += review.Rating;
            }
            return sum / reviews.Count;
        }
    }
}
