using Domain.Abstractions;
using Domain.Models;

namespace Data.Repositories
{
    public class FilmRepository : BaseRepository<Film>, IFilmRepository
    {
        public FilmRepository(MovieDbContext dbContext) : base(dbContext)
        {
        }
    }
}
