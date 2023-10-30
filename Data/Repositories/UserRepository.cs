using Domain.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(MovieDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> EmailExists(string email)
        {
            var emailExists = await _dbContext.Users.AnyAsync(u => u.Email == email);

            return emailExists;
        }
    }
}
