using Domain.Abstractions;
using Domain.Models;

namespace Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MovieDbContext dbContext) : base(dbContext)
        {
        }
    }
}
