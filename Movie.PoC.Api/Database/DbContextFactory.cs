using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Movie.PoC.Api.Database
{
    public class DbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var dbPath = Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "movie.db");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlite($"Data Source={dbPath}");


            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
