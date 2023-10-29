using Domain.Abstractions;
using Domain.Models;

namespace Data.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MovieDbContext dbContext) : base(dbContext)
        {
        }
    }
}
