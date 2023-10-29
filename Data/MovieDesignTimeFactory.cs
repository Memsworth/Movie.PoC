using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class MovieDesignTimeFactory : IDesignTimeDbContextFactory<MovieDbContext>
    {
        public MovieDbContext CreateDbContext(string[] args)
        {
            var optionBuilder = new DbContextOptionsBuilder<MovieDbContext>();
            optionBuilder.UseSqlite($"Data Source=Movie.db");
            return new MovieDbContext(optionBuilder.Options);
        }
    }
}
