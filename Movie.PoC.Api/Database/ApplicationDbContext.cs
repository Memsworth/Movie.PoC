using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Entities;
using Movie.PoC.Api.Features.Users;

namespace Movie.PoC.Api.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<UserModel> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());
        }
    }
}
