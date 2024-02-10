using Microsoft.EntityFrameworkCore;
using Movie.PoC.Api.Entities;
using Movie.PoC.Api.Features.Films;
using Movie.PoC.Api.Features.FilmsData;
using Movie.PoC.Api.Features.Reviews;
using Movie.PoC.Api.Features.Users;

namespace Movie.PoC.Api.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<FilmModel> Films { get; set; }
        public DbSet<FilmDataModel> FilmDatas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserModelConfiguration());
            modelBuilder.ApplyConfiguration(new FilmModelConfiguration());
            modelBuilder.ApplyConfiguration(new FilmDataModelConfiguration());
        }
    }
}
