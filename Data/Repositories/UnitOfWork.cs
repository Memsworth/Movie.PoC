using Domain.Abstractions;

namespace Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly MovieDbContext _dbContext;

        public IFilmRepository FilmRepository { get; private set; }

        public IUserRepository UserRepository { get; private set; }

        public IReviewRepository ReviewRepository { get; private set; }


        public UnitOfWork(MovieDbContext dbContext, IFilmRepository filmRepository, IUserRepository userRepository, IReviewRepository reviewRepository)
        {
            _dbContext = dbContext;
            FilmRepository = filmRepository;
            UserRepository = userRepository;
            ReviewRepository = reviewRepository;
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
